using Microsoft.Extensions.Logging;
using PropertyManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Infastructure.Persistence
{
    public class PropertyContextSeed
    {
        public static async Task SeedAsync(PropertyContext propertyContext, ILogger<PropertyContextSeed> logger)
        {
            if (!propertyContext.Properties.Any())
            {
                propertyContext.Properties.AddRange(GetPreconfiguredOrders());
                await propertyContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(PropertyContext).Name);
            }
        }

        private static IEnumerable<Property> GetPreconfiguredOrders()
        {
            return new List<Property>
            {
                new Property() {Name = "swn", Address = "Mehmet", ContactNo = "Ozkaya" }
            };
        }
    }
}
