using MediatR;
using System.Collections.Generic;

namespace PropertyManagement.Application.Features.Property.Queries.GetPropertyList
{
    public class GetPropertyListRequest : IRequest<List<PropertyVm>>
    {
        public string OwnerId { get; set; }

    }
}
