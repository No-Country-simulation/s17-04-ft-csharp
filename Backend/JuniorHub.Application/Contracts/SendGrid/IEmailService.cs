using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Employer;
using JuniorHub.Application.DTOs.Freelancer;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.Contracts.SendGrid
{
    public interface IEmailService
    {
        Task<bool> SendEmailToFreelancer(int freelancerId, int offerId, int employerId);
    }
}
