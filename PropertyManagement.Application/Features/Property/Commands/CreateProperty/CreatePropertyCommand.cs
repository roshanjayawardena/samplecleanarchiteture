using MediatR;

namespace PropertyManagement.Application.Features.Property.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }

    }
}
