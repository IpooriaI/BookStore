using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Books;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Categories.Contracts;
using Moq;
using Xunit;

namespace BookStore.Services.Test.Unit.Books
{
    public class BookServiceTestsWithMoq
    {

        [Fact]
        public void Add_should_add_books_Properly()
        {
            var category = new Category
            {
                Title = "sample",
                Id = 1
            };
            var dto = new AddBookDto
            {
                Title = "Dummy",
                Author = "test",
                Description = "test",
                Pages = 50,
                CategoryId = category.Id
            };
            var book = new Book
            {
                Title = "Dummy",
                Author = "test",
                Description = "test",
                Pages = 50,
                CategoryId = category.Id
            };
            Mock<CategoryRepository> categoryRepository = new Mock<CategoryRepository>();
            Mock<BookRepository> repository = new Mock<BookRepository>();
            Mock<UnitOfWork> unitOfWork = new Mock<UnitOfWork>();

            var sut = new BookAppService(repository.Object,unitOfWork.Object,categoryRepository.Object);
            categoryRepository.Setup(_ => _.GetById(category.Id)).Returns(category);
            sut.Add(dto);

            repository.Verify(_ => _.Add(It.Is<Book>(_ => _.Title == dto.Title)));

            unitOfWork.Verify(_ => _.Commit());
        }




    }
}
