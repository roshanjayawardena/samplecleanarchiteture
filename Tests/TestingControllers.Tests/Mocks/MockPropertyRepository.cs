using AutoFixture;
using Moq;
using PropertyManagement.Application.Contracts.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace TestingControllers.Tests.Mocks
{
    public static class MockPropertyRepository
    {

        public static Mock<IPropertyRepository> GeneratePropertyRepository()
        {

            var fixture = new Fixture();

            fixture.Customize<PropertyManagement.Domain.Entities.Property>(e => e
                                            .With(x => x.Id));
            var properties = fixture.CreateMany<PropertyManagement.Domain.Entities.Property>(3).ToList();

            var mockRepo = new Mock<IPropertyRepository>();

            mockRepo.Setup(r => r.GetPropertiesByUserId(It.IsAny<string>())).ReturnsAsync(properties);

            //mockRepo.Setup(r => r.AddAsync(It.IsAny<PropertyManagement.Domain.Entities.Property>())).ReturnsAsync((PropertyManagement.Domain.Entities.Property propertyType) =>
            //{
            //    properties.Add(propertyType);
            //    propertyType.Id = 100;
            //    return propertyType;
            //});

            return mockRepo;

        }

    }
}
