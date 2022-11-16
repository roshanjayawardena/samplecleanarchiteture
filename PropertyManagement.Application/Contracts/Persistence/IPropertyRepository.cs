using PropertyManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Contracts.Persistence
{
    public interface IPropertyRepository : IAsyncRepository<Property>
    {
        Task<IEnumerable<Property>> GetPropertiesByUserId(string userId);

    }
}
