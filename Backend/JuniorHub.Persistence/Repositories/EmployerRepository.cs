using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using AutoMapper;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class EmployerRepository : GenericRepository<Employer>, IEmployerRepository
{
    public EmployerRepository(JuniorHubContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<Employer?> GetProfileEmployer(int userId)
    {
        var employer = await _dbContext.Employers
            .Include(e => e.Offers) // Incluimos las ofertas relacionadas
            .Where(e => e.UserId == userId)
            .Select(e => new Employer
            {
                Id = e.Id,
                UserId = e.UserId,
                // Otros campos del employer que necesites
                Offers = e.Offers.Select(o => new Offer
                {
                    Title = o.Title,
                    Description = o.Description,
                    Price = o.Price,
                    EstimatedTime = o.EstimatedTime,
                    State = o.State,
                    Difficult = o.Difficult
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return employer;
    }

    public async Task<Employer?> GetEmployerForValoration(int userId)
    {
        var employer = await _dbContext.Employers
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        return employer;
    }

    public async Task<bool> EmployerIdExistsAsync(int id)
    {
        return await _dbContext.Employers
            .AnyAsync(f => f.Id == id);
    }
}