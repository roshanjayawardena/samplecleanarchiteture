using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Features.Property.Commands.UpdateProperty
{
    public class UpdatePropertyCommand: IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }


        public class Handler : IRequestHandler<UpdatePropertyCommand, int>
        {
            private readonly IPropertyRepository _propertyRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<UpdatePropertyCommand> _logger;

            public Handler(IPropertyRepository propertyRepository, IMapper mapper, ILogger<UpdatePropertyCommand> logger)
            {
                _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<int> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
            {

                var propertyToUpdate = await _propertyRepository.GetByIdAsync(request.Id);
                if (propertyToUpdate == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Property), request.Id);
                }

                _mapper.Map(request, propertyToUpdate, typeof(UpdatePropertyCommand), typeof(Domain.Entities.Property));

                await _propertyRepository.UpdateAsync(propertyToUpdate);

                _logger.LogInformation($"Order {propertyToUpdate.Id} is successfully updated.");

                return request.Id;
            }          
        }
    }
}
