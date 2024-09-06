using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Freelancer;
using JunioHub.Application.DTOs.Link;
using JunioHub.Application.DTOs.Technology;
using JuniorHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JunioHub.Application.Services
{
    public class FreelancerService : IFreelancerService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ITechnologyService _technologyService;
        private readonly IMapper _mapper;
        private readonly ILogger<FreelancerService> _logger;
        public FreelancerService(IFreelancerRepository freelancerRepository, UserManager<User> userManager, IMapper mapper, ILogger<FreelancerService> logger, ITechnologyService technologyService)
        {
            _freelancerRepository = freelancerRepository;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _technologyService = technologyService;
        }

        public async Task<BaseResponse<FreelancerDto>> AddFreelancer(FreelancerAddDto freelancer,int idUser)
        {
            BaseResponse<FreelancerDto> baseResponse;

            var existsFreelancer = await GetProfileFreelancer(idUser);
            if(existsFreelancer.Success)
            {
                baseResponse = new BaseResponse<FreelancerDto>(null, false, "Freelance already exists", null);
                return baseResponse;
            }

            

            try
            {
                var technologyNames = freelancer.Technologies.Select(t => t.Name).ToList();
                var existingTechnologies = (await _technologyService.GetAllTechnologies()).Data?.Where(t => technologyNames.Contains(t.Name)).ToList();

                if (existingTechnologies.Count != technologyNames.Count)
                {
                    baseResponse = new BaseResponse<FreelancerDto>(null, false, "Some technologies do not exist", null);
                    return baseResponse;
                }

                // Si alguna tecnología no existe en la base de datos, debes manejarlo de alguna manera
                if (existingTechnologies.Count != technologyNames.Count)
                {
                    baseResponse = new BaseResponse<FreelancerDto>(null, false, "Some technologies do not exist", null);
                    return baseResponse;
                }

                var freeLancer = new Freelancer()
                {
                    Description = freelancer.Description,
                    Links = freelancer.Links.Select(l => _mapper.Map<Link>(l)).ToList(),
                    Technologies = freelancer.Technologies.Select(t => _mapper.Map<Technology>(t)).ToList(),
                    Valoration = JuniorHub.Domain.Enums.ValorationEnum.Average,
                    UserId = idUser
                };

                var result = await _freelancerRepository.AddAsync(freeLancer);
                await _freelancerRepository.SaveChangesAsync();
                baseResponse = new BaseResponse<FreelancerDto>(_mapper.Map<FreelancerDto>(result),true, $"New freelancer of idUser:{idUser} added successfully.", null);
            }
            catch(Exception e)
            {
                baseResponse = new BaseResponse<FreelancerDto>(null, false, e.Message, null);
                _logger.LogError(e, e.Message);
            }

            return baseResponse;
        }

        public async Task<BaseResponse<FreelancerProfileDto>> GetProfileFreelancer(int idUser)
        {
            BaseResponse<FreelancerProfileDto> baseResponse;
            try
            {
                var freelancer = await _freelancerRepository.GetProfileFreelancer(idUser);
                var user = await _userManager.FindByIdAsync(idUser.ToString());

                var freelancerProfileDto = new FreelancerProfileDto()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Description = freelancer.Description,
                    Email = user.Email,
                    MediaUrl = user.MediaUrl,
                    ValorationEnum = freelancer.Valoration,
                    Technologies = freelancer.Technologies.Select(t => _mapper.Map<TechnologiesDto>(t)).ToList(),
                    Links = freelancer.Links.Select(l => _mapper.Map<LinkDto>(l)).ToList(),

                };

                baseResponse = new BaseResponse<FreelancerProfileDto>(freelancerProfileDto,true,"",null);

            }
            catch(Exception ex)
            {
                baseResponse = new BaseResponse<FreelancerProfileDto>(null, false,ex.Message, null);
                _logger.LogError(ex,ex.Message);
            }

            return baseResponse;

        }
    }
}
