using JunioHub.Application.DTOs.Freelancer;
using JunioHub.Application.DTOs;
using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Contracts.Persistence
{
    public interface IFreelancerRepository : IGenericRepository<Freelancer>
    {
        Task<Freelancer?> GetProfileFreelancer(int idUser);

    }
}
