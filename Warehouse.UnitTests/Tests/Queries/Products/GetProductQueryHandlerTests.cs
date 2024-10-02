
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Warehouse.Application.Queries.Orders;
using Warehouse.Application.Queries.Products;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;

namespace Warehouse.UnitTests.Tests.Queries.Products
{
    public class GetProductQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly GetProductQueryHandler _sut;
        public IFixture AutoFixture => new Fixture();

        public GetProductQueryHandlerTests()
        {
            _productRepository = new(MockBehavior.Strict);
            _sut = new GetProductQueryHandler(_productRepository.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Order_Exists_Should_Return_Product(GetProductQuery query)
        {
            //arrange
            var product = AutoFixture.Build<Product>()
                                    .With(x => x.Id, query.Id)
                                    .Create();

            _productRepository.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync(product);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _productRepository.Verify(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Product.Should().NotBeNull();
            result.Product.Should().BeEquivalentTo(product);
        }

        [Theory]
        [AutoData]
        public async Task When_Product_DoesNotExist_Should_Throw_Exception(GetProductQuery query)
        {
            //arrange
            _productRepository.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Product)null); ;

            //act
            Func<Task> result = () => _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            await result.Should().ThrowAsync<DataNotFoundException>();
        }
    }
}
