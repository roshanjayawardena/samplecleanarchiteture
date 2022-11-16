using PropertyManagement.Domain.Common;

namespace PropertyManagement.Domain.Entities
{
    public class Property : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }       
    }
}
