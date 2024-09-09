using JunioHub.Application.Contracts.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Employer;
using JunioHub.Application.Validators;
using JuniorHub.Domain.Entities;
using JunioHub.Application.Exceptions;
using Microsoft.AspNetCore.Identity;
using JunioHub.Application.DTOs.OfferDto;

namespace JunioHub.Application.Services;

public class EmployerService : IEmployerService
{
    private readonly IEmployerRepository _repository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<EmployerService> _logger;
    public EmployerService(
        IEmployerRepository repository,
        IMapper mapper,
        UserManager<User> userManager,
        ILogger<EmployerService> logger)
    {
        _userManager=userManager;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<EmployersDto>> AddEmployer(EmployerAddDto employerData,int idUser)
    {
        
        var baseResponse = new BaseResponse<EmployersDto>();

        var existsEmployer=await GetProfileEmployer(idUser);
         if(existsEmployer.Success)
        {
                baseResponse = new BaseResponse<EmployersDto>(null, false, "Employer already exists", null);
                return baseResponse;
        }

        if (baseResponse.Success)
        {
            var employer=new Employer()
            {
              Offers= employerData.Offers.Select(l=> _mapper.Map<Offer>(l)).ToList(),
              Valoration=JuniorHub.Domain.Enums.ValorationEnum.Average,
              UserId=idUser
            };

            var result=await _repository.AddAsync(employer);
            await _repository.SaveChangesAsync();
 
            baseResponse.Data = _mapper.Map<EmployersDto>(result);
            baseResponse.Message = "New employer added successfully.";
           
        }

        return baseResponse;
    }

    public async  Task<BaseResponse<EmployerUpdateDto>> UpdateEmployer(EmployerUpdateDto employerUpdateDto,int employerId)
    {
       BaseResponse<EmployerUpdateDto> baseResponse;

       try{
           var existingEmployer = await _repository.GetProfileEmployer(employerId);
                if (existingEmployer is null)
                {
                    baseResponse = new BaseResponse<EmployerUpdateDto>(null, false, "Employer not found", null);
                    return baseResponse;
                }

            existingEmployer.Valoration=employerUpdateDto.ValorationEnum;    
            var existingEmployerUser = await _userManager.FindByIdAsync(employerId.ToString());
            
            existingEmployerUser = _mapper.Map(employerUpdateDto, existingEmployerUser);
           
            var updateUserResult = await _userManager.UpdateAsync(existingEmployerUser);
           
            _repository.Update(existingEmployer);
            
            await _repository.SaveChangesAsync();
            
            var employerDto = _mapper.Map<EmployerUpdateDto>(existingEmployer);

            employerDto = _mapper.Map(existingEmployerUser, employerDto);
            baseResponse = new BaseResponse<EmployerUpdateDto>(employerDto, true, "Employer updated successfully.", null);

          }catch(Exception e){
           baseResponse = new BaseResponse<EmployerUpdateDto>(null, false, e.Message, null);
                _logger.LogError(e, e.Message);
          }

          return baseResponse;
    }

    public async Task<BaseResponse<EmployerProfileDto>> GetProfileEmployer(int idUser)
    {
        var baseResponse = new BaseResponse<EmployerProfileDto>();

        try
        {
           var employer = await _repository.GetProfileEmployer(idUser);
            if (employer is null)
            {
                return baseResponse = new BaseResponse<EmployerProfileDto>(null, false, "This user does not have a employer", null);
            }

            var user = await _userManager.FindByIdAsync(idUser.ToString());

            var employerProfileDto = new EmployerProfileDto()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    MediaUrl = user.MediaUrl,
                    ValorationEnum = employer.Valoration,
                    Offers=employer.Offers.Select(t=>_mapper.Map<OfferGetWhereDto>(t)).ToList(),
                };

                baseResponse = new BaseResponse<EmployerProfileDto>(employerProfileDto,true,"",null);
        }
        catch (Exception ex)
        {
           baseResponse = new BaseResponse<EmployerProfileDto>(null, false,ex.Message, null);
                _logger.LogError(ex,ex.Message);
        }

        return baseResponse;
    }

}
