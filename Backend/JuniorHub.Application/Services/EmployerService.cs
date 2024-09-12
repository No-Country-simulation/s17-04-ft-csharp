using JuniorHub.Application.Contracts.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Employer;
using JuniorHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using JuniorHub.Application.DTOs.Offer;

namespace JuniorHub.Application.Services;

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
