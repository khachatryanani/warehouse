using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Warehouse.Application.Queries.Categories;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;

namespace Warehouse.UnitTests.Tests.Queries.Categories
{
    public class GetCategoriesQueryHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly GetCategoriesQueryHandler _sut;

        public GetCategoriesQueryHandlerTests()
        {
            _categoryRepository = new(MockBehavior.Strict);
            _sut = new GetCategoriesQueryHandler(_categoryRepository.Object);
        }

        [Theory]
        [AutoData]
        public async Task Should_Return_Categories(List<Category> categories, GetCategoriesQuery query)
        {
            //arrange
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(categories);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _categoryRepository.Verify(x => x.GetAsync(It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Categories.Should().NotBeNull();
            result.Categories.Count().Should().Be(categories.Count);
            result.Categories.Should().BeEquivalentTo(categories);
        }
    }
}
