using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Freelancer;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Persistence.Repositories
{
    public class FreelancerRepository : GenericRepository<Freelancer>, IFreelancerRepository
    {
        public FreelancerRepository(JuniorHubContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {
        }

        public async Task<Freelancer?> GetProfileFreelancer(int idUser)
        {
            var user = await _dbContext.Freelancers.Include(f=>f.Technologies).Include(f=>f.Links).FirstOrDefaultAsync(f=>f.UserId == idUser);
            
            return user;
        }
    }
}
