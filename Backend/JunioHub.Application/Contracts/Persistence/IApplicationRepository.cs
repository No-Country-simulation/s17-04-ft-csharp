using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Contracts.Persistence
{
    public interface IApplicationRepository : IGenericRepository<JuniorHub.Domain.Entities.Application>
    {
    }
}
