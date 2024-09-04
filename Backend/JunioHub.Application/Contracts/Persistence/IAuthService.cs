using JunioHub.Application.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Contracts.Persistence
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterAsync(RegisterDto register);
        public Task<(IdentityResult, string?)> LoginAsync(LoginDto login);
    }
}
