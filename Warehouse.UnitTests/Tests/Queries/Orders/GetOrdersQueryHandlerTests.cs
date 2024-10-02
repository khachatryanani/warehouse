using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Warehouse.Application.Queries.Orders;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;

namespace Warehouse.UnitTests.Tests.Queries.Orders
{
    public class GetOrdersQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly GetOrdersQueryHandler _sut;
        public IFixture AutoFixture => new Fixture();

        public GetOrdersQueryHandlerTests()
        {
            _orderRepository = new(MockBehavior.Strict);
            _sut = new GetOrdersQueryHandler(_orderRepository.Object);
        }

        [Theory]
        [AutoData]
        public async Task Should_Return_Orders(List<Order> orders, GetOrdersQuery query)
        {
            //arrange
            _orderRepository.Setup(x => x.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(orders);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _orderRepository.Verify(x => x.GetAsync(It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Orders.Should().NotBeNull();
            result.Orders.Count().Should().Be(orders.Count);
            result.Orders.Should().BeEquivalentTo(orders);
        }

        [Theory]
        [AutoData]
        public async Task Should_Return_Orders_By_UserId(GetOrdersByUserIdQuery query)
        {
            //arrange
            var orders = AutoFixture.Build<Order>()
                                    .With(x => x.UserId, query.UserId)
                                    .CreateMany();

            _orderRepository.Setup(x => x.GetByUserIdAsync(query.UserId, It.IsAny<CancellationToken>())).ReturnsAsync(orders);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _orderRepository.Verify(x => x.GetByUserIdAsync(query.UserId, It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Orders.Should().NotBeNull();
            result.Orders.Count().Should().Be(orders.Count());
            result.Orders.Should().BeEquivalentTo(orders);
        }
    }
}
