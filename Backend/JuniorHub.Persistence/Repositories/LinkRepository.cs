using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
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
    public class LinkRepository : GenericRepository<Link>, ILinkRepository
    {
        public LinkRepository(JuniorHubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool> LinkExistsAsync(int idLink)
        {
            return await _dbContext.Links.AnyAsync(l => l.Id == idLink);
        }
    }
}
