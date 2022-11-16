using Microsoft.EntityFrameworkCore;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Domain.Entities;
using PropertyManagement.Infastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyManagement.Infastructure.Repositories
{
    public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {

        public PropertyRepository(PropertyContext dbContext) : base(dbContext)
        {
        }    

        public async Task<IEnumerable<Property>> GetPropertiesByUserId(string userId)
        {
            var propertyList = await _dbContext.Properties
                               .Where(o => o.CreatedBy == userId)
                               .ToListAsync();
            return propertyList;
        }
    }
}
