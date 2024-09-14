using JuniorHub.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.Contracts.SendGrid
{
    public interface IEmailService
    {
        Task<BaseResponse<bool>> SendGridEmailAsync(string emailTo, string emailSubject, string emailBody);
    }
}
