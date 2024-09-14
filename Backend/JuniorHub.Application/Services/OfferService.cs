using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Freelancer;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.Services
{
    internal class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IMapper _mapper;
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<OfferService> _logger;
        private readonly IOfferApplicationRepository _applicationRepository;
        public OfferService(IOfferRepository offerRepository, ITechnologyRepository technologyRepository, IMapper mapper, IEmployerRepository employerRepository, ILogger<OfferService> logger, IOfferApplicationRepository applicationRepository)
        {
            _offerRepository = offerRepository;
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _employerRepository = employerRepository;
            _logger = logger;
            _applicationRepository = applicationRepository;
        }
        public async Task<BaseResponse<OfferGetByIdDto>> AddOffer(OfferAddDto offerAddDto,int idUser)
        {
            BaseResponse<OfferGetByIdDto> baseResponse;

            var technologyNames = offerAddDto.Technologies.Select(t => t.Name).ToList();
            var existingTechnologies = (await _technologyRepository.GetAllAsync())
                .Where(t => technologyNames.Contains(t.Name))
                .ToList();

            if (existingTechnologies.Count != technologyNames.Count)
            {
                baseResponse = new BaseResponse<OfferGetByIdDto>(null, false, "Some technologies do not exist", null);
                return baseResponse;
            }
            try
            {
                var offerToAdd = _mapper.Map<Offer>(offerAddDto);
                offerToAdd.EmployerId = (await _employerRepository.GetProfileEmployer(idUser)).Id;
                offerToAdd.Technologies = existingTechnologies;
                var result = await _offerRepository.AddAsync(offerToAdd);
                await _offerRepository.SaveChangesAsync();


                var offer = await _offerRepository.GetFullOfferAsync(offerToAdd.Id);
                var offerDto = _mapper.Map<OfferGetByIdDto>(offerToAdd);
                offerDto.FullNameAuthor = string.Concat(offer.Employer.User.Name, " ", offer.Employer.User.LastName);
                offerDto.UserId = offer.Employer.UserId;
                baseResponse = new BaseResponse<OfferGetByIdDto>(offerDto, true, null, null);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                baseResponse = new BaseResponse<OfferGetByIdDto>(null, false, ex.Message, null);
            }
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> DeleteOffer(int idOffer, int idUser)
        {
            BaseResponse<bool> baseResponse;
            try
            {
                var existingOffer = await _offerRepository.GetFullOfferAsync(idOffer);

                if (existingOffer is null)
                {
                    baseResponse = new BaseResponse<bool>(false, false, "Offer not found", null);
                    return baseResponse;
                }

                var idEmployer = (await _employerRepository.GetProfileEmployer(idUser)).Id;

                if (existingOffer.EmployerId != idEmployer)
                {
                    baseResponse = new BaseResponse<bool>(false, false, "Offer not found", null);
                    return baseResponse;
                }

                var applications = (await _applicationRepository.GetAllAsync()).Where(a=>a.OfferId==idOffer);

                foreach(var app in applications)
                {
                    var deleteResult = await _applicationRepository.DeleteAsync(app.Id);
                }

                var result = await _offerRepository.DeleteAsync(idOffer);
                await _offerRepository.SaveChangesAsync();

                if(!result)
                    baseResponse = new BaseResponse<bool>(false, false, $"Error in database to delete an offer with id:{idOffer}", null);
                baseResponse = new BaseResponse<bool>(true, true, "Offer deleted successfully", null);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                baseResponse = new BaseResponse<bool>(false, false, ex.Message, null);
            }

            return baseResponse;

        }

        public async Task<BaseResponse<OfferGetByIdDto>> GetOffer(int idOffer)
        {
            BaseResponse<OfferGetByIdDto> baseResponse;
            var offer = await _offerRepository.GetFullOfferAsync(idOffer);

            if(offer is null)
            {
                baseResponse = new BaseResponse<OfferGetByIdDto>(null, false, $"Not exists an offer with this id:{idOffer}",null);
                return baseResponse;
            }
            var offerDto = _mapper.Map<OfferGetByIdDto>(offer);
            offerDto.FullNameAuthor = string.Concat(offer.Employer.User.Name, " ", offer.Employer.User.LastName);
            offerDto.UserId = offer.Employer.UserId;
            baseResponse = new BaseResponse<OfferGetByIdDto>(offerDto,true,null,null);
            return baseResponse;
        }

        public async Task<BaseResponse<OffersPagedDto>> GetOffers(string? title, string? technology,int page)
        {
            var offers = _offerRepository.GetAllOfferQuery();

            if (!string.IsNullOrEmpty(title))
            {
                offers = offers.Where(o => o.Title.ToLower().Contains(title.ToLower()));
            }

            if (!string.IsNullOrEmpty(technology))
            {
                offers = offers.Where(o => o.Technologies.Any(t => t.Name.ToLower().Contains(technology.ToLower())));
            }
            var result = offers.Skip((page - 1) * 20).Take(20).ToList();

            int totalPosts = result.Count();
            int totalPages = (int)Math.Ceiling((double)totalPosts / 20);


            var offerResult = new OffersPagedDto()
            {
                currentPage = page,
                totalPages = totalPages,
                Offers = result.Select(o => _mapper.Map<OfferDto>(o)).ToList()
            };

            return new BaseResponse<OffersPagedDto>(offerResult,true,null,null);
        }

        public async Task<BaseResponse<OffersPagedDto>> GetOffers(string? search, int page)
        {
            var offers = _offerRepository.GetAllOfferQuery().Where(o => o.State == State.Open);

            if (!string.IsNullOrEmpty(search))
            {
                offers = offers.Where(o => o.Title.ToLower().Contains(search.ToLower())
                    || o.Technologies.Any(t => t.Name.ToLower().Contains(search.ToLower())));
            }

            var result = offers.Skip((page - 1) * 20).Take(20).ToList();

            int totalPosts = result.Count();
            int totalPages = (int)Math.Ceiling((double)totalPosts / 20);

            var offerResult = new OffersPagedDto()
            {
                currentPage = page,
                totalPages = totalPages,
                Offers = result.Select(o => _mapper.Map<OfferDto>(o)).ToList()
            };

            return new BaseResponse<OffersPagedDto>(offerResult, true, null, null);
        }

        public async Task<BaseResponse<OfferUpdateDto>> UpdateOffer(OfferUpdateDto offerUpdateDto, int idOffer, int idUser)
        {
            BaseResponse<OfferUpdateDto> baseResponse;

            try
            {
                var existingOffer = await _offerRepository.GetFullOfferAsync(idOffer);

                if (existingOffer is null)
                {
                    baseResponse = new BaseResponse<OfferUpdateDto>(null, false, "Offer not found", null);
                    return baseResponse;
                }

                var idEmployer = (await _employerRepository.GetProfileEmployer(idUser)).Id;

                if (existingOffer.EmployerId != idEmployer)
                {
                    baseResponse = new BaseResponse<OfferUpdateDto>(null, false, "Offer not found", null);
                    return baseResponse;
                }    

                var technologyNames = offerUpdateDto.Technologies.Select(t => t.Name).ToList();

                var existingTechnologies = (await _technologyRepository.GetAllAsync())
                    .Where(t => technologyNames.Contains(t.Name))
                    .ToList();

                if (existingTechnologies.Count != technologyNames.Count)
                {
                    baseResponse = new BaseResponse<OfferUpdateDto>(null, false, "Some technologies do not exist", null);
                    return baseResponse;
                }

                _mapper.Map(offerUpdateDto, existingOffer);

                existingOffer.Technologies.Clear();  
                existingOffer.Technologies = existingTechnologies;  

                _offerRepository.Update(existingOffer);
                await _offerRepository.SaveChangesAsync();

                var offerUpdatedDto = _mapper.Map<OfferUpdateDto>(existingOffer);
                baseResponse = new BaseResponse<OfferUpdateDto>(offerUpdatedDto, true, "Offer updated successfully", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                baseResponse = new BaseResponse<OfferUpdateDto>(null, false, ex.Message, null);
            }

            return baseResponse;
        }
    }
}
