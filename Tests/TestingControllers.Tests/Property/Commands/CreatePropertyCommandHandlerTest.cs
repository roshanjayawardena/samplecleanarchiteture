using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using PropertyManagement.Application.Contracts.Infastructure;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Application.Features.Property.Commands.CreateProperty;
using PropertyManagement.Application.Mappings;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using TestingControllers.Tests.Mocks;
using Xunit;

namespace TestingControllers.Tests.Property.Commands
{
    public class CreatePropertyCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPropertyRepository> _mockRepo;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<ILogger<CreatePropertyCommand>> _logger;
        private readonly CreatePropertyCommandHandler _handler;
        public CreatePropertyCommandHandlerTest()
        {
            _logger = new Mock<ILogger<CreatePropertyCommand>>();
            _mockRepo = MockPropertyRepository.GeneratePropertyRepository();
            _mockEmailService = new Mock<IEmailService>();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreatePropertyCommandHandler(_mockRepo.Object, _mapper, _mockEmailService.Object, _logger.Object);
        }

        [Fact]
        public async Task Valid_Property_Added()
        {

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<PropertyManagement.Domain.Entities.Property>())).ReturnsAsync((PropertyManagement.Domain.Entities.Property propertyType) =>
            {
                propertyType.Id = 100;
                return propertyType;
            });

            var result = await _handler.Handle(new CreatePropertyCommand() { Name = "Araliya Resort", Address = "No 18/A Temple road,Colombo", ContactNo = "078654321" }, CancellationToken.None);

            result.ShouldBeOfType<int>();
            Assert.Equal(100, result);
        }

        [Fact]
        public async Task InValid_Property_Added()
        {

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<PropertyManagement.Domain.Entities.Property>())).ReturnsAsync((PropertyManagement.Domain.Entities.Property propertyType) =>
            {
                propertyType.Id = 0;
                return propertyType;
            });

            var result = await _handler.Handle(new CreatePropertyCommand() { Name = "Araliya Resort", Address = "No 18/A Temple road,Colombo", ContactNo = "078654321" }, CancellationToken.None);


            result.ShouldBeOfType<int>();

            Assert.Equal(0, result);

            // appsetting file ekak dagana widiya
            // should eke checks 
        }
    }
}
