using AutoMapper;
using PropertyManagement.Application.Features.Property.Commands.CreateProperty;
using PropertyManagement.Application.Features.Property.Commands.UpdateProperty;
using PropertyManagement.Application.Features.Property.Queries.GetPropertyList;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Property, PropertyVm>().ReverseMap();
            CreateMap<Property, CreatePropertyCommand>().ReverseMap();
            CreateMap<Property, UpdatePropertyCommand>().ReverseMap();
        }
    }
}
