
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Warehouse.Application.Queries.Categories;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Exceptions;

namespace Warehouse.UnitTests.Tests.Queries.Categories
{
    public class GetCategoryQueryHandlerTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly GetCategoryQueryHandler _sut;
        public IFixture AutoFixture => new Fixture();

        public GetCategoryQueryHandlerTest()
        {
            _categoryRepository = new(MockBehavior.Strict);
            _sut = new GetCategoryQueryHandler(_categoryRepository.Object);
        }

        [Theory]
        [AutoData]
        public async Task When_Category_Exists_Should_Return_Category(GetCategoryQuery query)
        {
            //arrange
            var category = AutoFixture.Build<Category>()
                                      .With(x => x.Id, query.Id)
                                      .Create();

            _categoryRepository.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);

            //act
            var result = await _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            _categoryRepository.Verify(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once());

            result.Should().NotBeNull();
            result.Category.Should().NotBeNull();
            result.Category.Should().BeEquivalentTo(category);
        }

        [Theory]
        [AutoData]
        public async Task When_Category_DoesNotExist_Should_Throw_Exception(GetCategoryQuery query)
        {
            //arrange
            _categoryRepository.Setup(x => x.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Category)null); ;

            //act
            Func<Task> result = () => _sut.Handle(query, It.IsAny<CancellationToken>());

            //assert
            await result.Should().ThrowAsync<DataNotFoundException>();
        }
    }
}
