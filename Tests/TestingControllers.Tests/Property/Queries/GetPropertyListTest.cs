using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using PropertyManagement.Application.Contracts.Persistence;
using PropertyManagement.Application.Features.Property.Queries.GetPropertyList;
using PropertyManagement.Application.Mappings;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestingControllers.Tests.Mocks;
using Xunit;

namespace TestingControllers.Tests.Property.Queries
{
    public class GetPropertyListTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPropertyRepository> _mockRepo;
        public GetPropertyListTest()
        {
            _mockRepo = MockPropertyRepository.GeneratePropertyRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Theory]
        [AutoData]
        public async Task GetPropertyListByUserIdTest(string ownerId)
        {
            var handler = new GetPropertyListRequestHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetPropertyListRequest() { OwnerId = ownerId }, CancellationToken.None);

            result.ShouldBeOfType<List<PropertyVm>>();

            result.Count.ShouldBe(3);
        }

    }
}
