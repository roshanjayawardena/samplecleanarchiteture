using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Features.Property.Commands.DeleteProperty
{
    public class DeletePropertyCommand : IRequest
    {

        public int Id { get; set; }

        public class Handler : IRequestHandler<DeletePropertyCommand>
        {
            private readonly IPropertyRepository _propertyRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<DeletePropertyCommand> _logger;

            public Handler(IPropertyRepository propertyRepository, IMapper mapper, ILogger<DeletePropertyCommand> logger)
            {
                _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Unit> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
            {
                var orderToDelete = await _propertyRepository.GetByIdAsync(request.Id);
                if (orderToDelete == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.Property), request.Id);
                }

                await _propertyRepository.DeleteAsync(orderToDelete);

                _logger.LogInformation($"Property {orderToDelete.Id} is successfully deleted.");
                return Unit.Value;

            }


        }

    }
}
