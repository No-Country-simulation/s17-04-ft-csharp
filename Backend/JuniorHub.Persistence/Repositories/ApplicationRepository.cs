using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Persistence.Repositories
{
    internal class ApplicationRepository : GenericRepository<Application>,IApplicationRepository
    {
        public ApplicationRepository(JuniorHubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
