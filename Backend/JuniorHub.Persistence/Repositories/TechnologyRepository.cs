using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;

namespace JuniorHub.Persistence.Repositories;

public class TechnologyRepository : GenericRepository<Technology>, ITechnologyRepository
{
    public TechnologyRepository(JuniorHubContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}