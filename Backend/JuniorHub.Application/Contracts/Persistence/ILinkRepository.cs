using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.Contracts.Persistence
{
    public interface ILinkRepository : IGenericRepository<Link>
    {
        Task<bool> LinkExistsAsync(int idLink);
    }
}
