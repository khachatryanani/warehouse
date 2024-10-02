using AutoFixture.Xunit2;
using AutoFixture;
using FluentAssertions;
using Moq;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;
using Warehouse.Application.Queries.Orders;

namespace Warehouse.UnitTests.Tests.Queries.Orders
{
    public class GetOrderQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly GetOrderQueryHandler _sut;
        public IFixture AutoFixture => new Fixture();

        public GetOrderQueryHandlerTests()
        {
            _orderRepository = new(MockBehavior.Strict);
            _sut = new GetOrderQueryHandler(_orderRepository.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Order_Exists_Should_Return_Order(GetOrderQuery query)
        {
            //arrange
            var order = AutoFixture.Build<Order>()
                                    .With(x => x.Id, query.Id)
                                    .Create();

            _orderRepository.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync(order);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _orderRepository.Verify(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Order.Should().NotBeNull();
            result.Order.Should().BeEquivalentTo(order);
        }

        [Theory]
        [AutoData]
        public async Task When_Order_DoesNotExist_Should_Throw_Exception(GetOrderQuery query)
        {
            //arrange
            _orderRepository.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Order)null); ;

            //act
            Func<Task> result = () => _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            await result.Should().ThrowAsync<DataNotFoundException>();
        }
    }
}
