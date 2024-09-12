using AutoMapper;
using AutoMapper.QueryableExtensions;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly JuniorHubContext _dbContext;
    private readonly IMapper _mapper;

    public GenericRepository(JuniorHubContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GenericRepository(JuniorHubContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<TDto?> GetByIdAsyncProjectTo<TDto>(int id) where TDto : class
    {
        var entity = await _dbContext.Set<T>()
            .Where(e => EF.Property<int>(e, "Id") == id)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return entity;
    }

    public async Task<IEnumerable<TDto?>> GetByPropertyAsyncProjectTo<TDto>(string propertyName, object value) where TDto : class
    {
        var entity = await _dbContext.Set<T>()
            .Where(e => EF.Property<object>(e, propertyName).Equals(value))
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return entity;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<List<TDto?>> GetAllAsyncProjectTo<TDto>() where TDto : class
    {
        var entity = await _dbContext.Set<T>()
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return entity;
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity == null)
        {
            return false;
        }
        _dbContext.Set<T>().Remove(entity);

        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
