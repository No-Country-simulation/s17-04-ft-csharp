﻿using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Freelancer;
using JuniorHub.Application.DTOs.Link;
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JuniorHub.Application.Services;

public class FreelancerService : IFreelancerService
{
    private readonly UserManager<User> _userManager;
    private readonly IFreelancerRepository _freelancerRepository;
    private readonly ITechnologyRepository _technologyRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FreelancerService> _logger;
    private readonly ILinkRepository _linkRepository;

    public FreelancerService(
        IFreelancerRepository freelancerRepository, 
        UserManager<User> userManager,
        IMapper mapper, ILogger<FreelancerService> logger, 
        ITechnologyRepository technologyRepository, 
        ILinkRepository linkRepository)
    {
        _freelancerRepository = freelancerRepository;
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
        _technologyRepository = technologyRepository;
        _linkRepository = linkRepository;
    }

    public async Task<BaseResponse<FreelancerDto>> AddFreelancer(int idUser)
    {
        BaseResponse<FreelancerDto> baseResponse;

        var existsFreelancer = await GetProfileFreelancer(idUser);
        if(existsFreelancer.Success)
        {
            baseResponse = new BaseResponse<FreelancerDto>(
                null, 
                false, 
                "Freelance already exists", 
                null);
            return baseResponse;
        }
        try
        {
            var githubLink = new Link
            {
                Name = "GitHub",
                Url = "", 
                FreelancerId = idUser 
            };

            var linkedInLink = new Link
            {
                Name = "LinkedIn",
                Url = "", 
                FreelancerId = idUser 
            };

            var portfolioLink = new Link
            {
                Name = "Portafolio",
                Url = "", 
                FreelancerId = idUser 
            };

            var freeLancer = new Freelancer()
            {
                Description = " ",
                Links = new List<Link>() { githubLink, linkedInLink, portfolioLink },
                Technologies = new List<Technology>(),
                Valoration = ValorationEnum.Average,
                UserId = idUser
            };

            var result = await _freelancerRepository.AddAsync(freeLancer);
            await _freelancerRepository.SaveChangesAsync();
            baseResponse = new BaseResponse<FreelancerDto>(
                null,
                true, 
                $"New freelancer of idUser:{idUser} added successfully.", 
                null);
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
            if(freelancer is null)
            {
                return baseResponse = new BaseResponse<FreelancerProfileDto>(null, false, "This user does not have a freelancer", null);
            }
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

    public async Task<BaseResponse<FreelancerProfileDto>> UpdateFreelancer(FreelancerUpdateDto freelancerUpdateDto, int idUser)
    {
        BaseResponse<FreelancerProfileDto> baseResponse;

        try
        {
            var existingFreelancer = await _freelancerRepository.GetProfileFreelancer(idUser);
            if (existingFreelancer is null)
            {
                baseResponse = new BaseResponse<FreelancerProfileDto>(null, false, "Freelancer not found", null);
                return baseResponse;
            }

            var technologyNames = freelancerUpdateDto.Technologies.Select(t => t.Name).ToList();
            var existingTechnologies = (await _technologyRepository.GetAllAsync())
                .Where(t => technologyNames.Contains(t.Name))
                .ToList();

            if (existingTechnologies.Count != technologyNames.Count)
            {
                baseResponse = new BaseResponse<FreelancerProfileDto>(null, false, "Some technologies do not exist", null);
                return baseResponse;
            }

            foreach(var link in freelancerUpdateDto.Links)
            {
                if(link.Id !=0 && !(await _linkRepository.LinkExistsAsync(link.Id,existingFreelancer.Id)))
                {
                    baseResponse = new BaseResponse<FreelancerProfileDto>(null, false, "Some links do not exist", null);
                    return baseResponse;
                }
                
            }


            var existingFreelancerUser = await _userManager.FindByIdAsync(idUser.ToString());

            existingFreelancerUser = _mapper.Map(freelancerUpdateDto, existingFreelancerUser);

            existingFreelancer.Technologies.Clear();
            existingFreelancer.Technologies = existingTechnologies;
            existingFreelancer.Description = freelancerUpdateDto.Description;
            var updatedLinks = freelancerUpdateDto.Links;

            var linksToRemove = existingFreelancer.Links
                .Where(existingLink => updatedLinks.All(updatedLink => updatedLink.Id != existingLink.Id))
                .ToList();

            foreach (var linkToRemove in linksToRemove)
            {
                existingFreelancer.Links.Remove(linkToRemove);
            }

            // Actualizar o agregar enlaces
            foreach (var updatedLink in updatedLinks)
            {
                var existingLink = existingFreelancer.Links.FirstOrDefault(l => l.Id == updatedLink.Id);
                if (existingLink is not null)
                {
                    existingLink.Url = updatedLink.Url;
                    existingLink.Name = updatedLink.Name;
                }
                else
                {
                    existingFreelancer.Links.Add(_mapper.Map<Link>(updatedLink));
                }
            }

            var updateUserResult = await _userManager.UpdateAsync(existingFreelancerUser);
            _freelancerRepository.Update(existingFreelancer);
            await _freelancerRepository.SaveChangesAsync();


            var freelancerProfileDto = _mapper.Map<FreelancerProfileDto>(existingFreelancer);

            freelancerProfileDto = _mapper.Map(existingFreelancerUser, freelancerProfileDto);
            baseResponse = new BaseResponse<FreelancerProfileDto>(freelancerProfileDto,true,"Freelancer updated successfully.",null);
        }
        catch (Exception e)
        {
            baseResponse = new BaseResponse<FreelancerProfileDto>(null, false, e.Message, null);
            _logger.LogError(e, e.Message);
        }

        return baseResponse;
    }
}
