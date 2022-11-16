using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PropertyManagement.Application.Contracts.Infastructure;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Application.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PropertyManagement.Application.Features.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreatePropertyCommand> _logger;
        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper, IEmailService emailService, ILogger<CreatePropertyCommand> logger)
        {
            _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {

            var propertyEntity = _mapper.Map<Domain.Entities.Property>(request);
            var newProperty = await _propertyRepository.AddAsync(propertyEntity);

            _logger.LogInformation($"Property {newProperty.Id} is successfully created.");

            await SendMail(newProperty);

            return newProperty.Id;
        }

        private async Task SendMail(Domain.Entities.Property property)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {property.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
