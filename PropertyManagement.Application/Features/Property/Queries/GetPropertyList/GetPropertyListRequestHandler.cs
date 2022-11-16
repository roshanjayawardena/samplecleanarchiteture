using AutoMapper;
using MediatR;
using PropertyManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Features.Property.Queries.GetPropertyList
{
    public class GetPropertyListRequestHandler : IRequestHandler<GetPropertyListRequest, List<PropertyVm>>
    {

        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertyListRequestHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<PropertyVm>> Handle(GetPropertyListRequest request, CancellationToken cancellationToken)
        {
            var propertyList = await _propertyRepository.GetPropertiesByUserId(request.OwnerId);
            return _mapper.Map<List<PropertyVm>>(propertyList);
        }
    }
}
