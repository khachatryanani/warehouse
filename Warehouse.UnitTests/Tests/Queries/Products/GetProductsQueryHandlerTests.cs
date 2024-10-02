using AutoFixture.Xunit2;
using AutoFixture;
using Moq;
using Warehouse.Application.Queries.Orders;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Application.Queries.Products;
using FluentAssertions;

namespace Warehouse.UnitTests.Tests.Queries.Products
{
    public class GetProductsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly GetProductsQueryHandler _sut;
        public IFixture AutoFixture => new Fixture();

        public GetProductsQueryHandlerTests()
        {
            _productRepository = new(MockBehavior.Strict);
            _sut = new GetProductsQueryHandler(_productRepository.Object);
        }

        [Theory]
        [AutoData]
        public async Task Should_Return_Products(List<Product> products, GetProductsQuery query)
        {
            //arrange
            _productRepository.Setup(x => x.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(products);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _productRepository.Verify(x => x.GetAsync(It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Products.Should().NotBeNull();
            result.Products.Count().Should().Be(products.Count);
            result.Products.Should().BeEquivalentTo(products);
        }
    }
}
