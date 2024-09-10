using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence
{
    public interface IEmployerRepository :IGenericRepository<Employer>
    {
        Task<Employer?> GetProfileEmployer(int idUser);
    }
}