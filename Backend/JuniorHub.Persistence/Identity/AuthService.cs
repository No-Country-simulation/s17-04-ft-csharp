using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Identity;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Helpers;
using JuniorHub.Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Persistence.Identity
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmployerRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IFreelancerService _freelancerService;
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IEmployerRepository repository, JwtConfiguration jwtConfiguration, IFreelancerService freelancerService)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration;
            _freelancerService = freelancerService;
        }


        public async Task<(IdentityResult, string?)> LoginAsync(LoginDto login)
        {
            var emailExist = await _userManager.FindByEmailAsync(login.Email);

            if (emailExist is null || !(await _userManager.CheckPasswordAsync(emailExist, login.Password)))
                return (IdentityResult.Failed(new IdentityError() { Description = "Invalid email and/or password" }),null);

            var signInResult = await _signInManager.PasswordSignInAsync(emailExist, login.Password, false, false);

            if(!signInResult.Succeeded)
                return (IdentityResult.Failed(new IdentityError() { Description = "Error to attempt sign-in" }), null);

            var roleUser = await _userManager.GetRolesAsync(emailExist);

            return (IdentityResult.Success, GetToken(emailExist, roleUser.First()));
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto register)
        {
            var userExists = await _userManager.FindByEmailAsync(register.Email);

            if (userExists is not null)
                return IdentityResult.Failed(new IdentityError() { Description = "Email alreaedy in use" });

            var userToRegister = new User
            {
                Name = register.Name,
                Email = register.Email,
                LastName = register.LastName,
                UserName = register.Email
            };

            var createUserResult = await _userManager.CreateAsync(userToRegister, register.Password);

            if(!createUserResult.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError() { Description = string.Join(", ",createUserResult.Errors.Select(e=>e.Description))});
            }

            var addRoleResult = await AddRoleToUser(userToRegister, register.Role);

            if (!addRoleResult.Succeeded)
                return addRoleResult;

            if (register.Role is Role.Employer)
            {    
                try
                {
                   var employer=new Employer()
                   {
                    UserId=userToRegister.Id,
                   };
                  
                   var addEmployerResult=await _repository.AddAsync(employer);
                   await _repository.SaveChangesAsync();

                   if (addEmployerResult==null)
                   {
                      return IdentityResult.Failed(new IdentityError() 
                    { 
                      Description = "Could not save employer data, error" 
                    });
                  }
                }
                catch(Exception ex)
                {
                  return IdentityResult.Failed(new IdentityError() 
                  {
                     Description = $"An error occurred: {ex.Message}" 
                  });
                }
            }
            else if(register.Role is Role.Freelancer)
            {
                try
                {
                    var result =  await _freelancerService.AddFreelancer(userToRegister.Id);
                    if(!result.Success)
                    {
                        return IdentityResult.Failed(new IdentityError()
                        {
                            Description = $"An error occurred: {result.Message}"
                        });
                    }
                }
                catch (Exception ex)
                {
                    return IdentityResult.Failed(new IdentityError()
                    {
                        Description = $"An error occurred: {ex.Message}"
                    });
                }
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> AddRoleToUser(User user,Role role)
        {
            var addRoleResult = await _userManager.AddToRoleAsync(user, role.ToStringEnum());
            if(!addRoleResult.Succeeded)
            {
                var deleteUserResult = await _userManager.DeleteAsync(user);
                if(!deleteUserResult.Succeeded)
                    return IdentityResult.Failed(new IdentityError() { Description = string.Join(", ", deleteUserResult.Errors.Select(e => e.Description), addRoleResult.Errors.Select(e => e.Description))  });
                return IdentityResult.Failed(new IdentityError() { Description = string.Join(", ", addRoleResult.Errors.Select(e => e.Description)) });
            }
            return IdentityResult.Success;
        }

        public string GetToken(User user,string role)
        {
            byte[] key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: GetClaims(user, role),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddHours(_jwtConfiguration.DurationInHours)
            ) ;
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public List<Claim> GetClaims(User user, string role)
        {
            var claimList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim("name",user.Name),
                new Claim("lastName",user.LastName),
                new Claim("email",user.Email),
                new Claim(ClaimTypes.Role,role)
            };
            return claimList;
        }
    }
}
