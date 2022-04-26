using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Infrastructure.Test;
using BookStore.Persistence.EF;
using BookStore.Persistence.EF.Books;
using BookStore.Persistence.EF.Categories;
using BookStore.Services.Books;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Books.Exceptions;
using BookStore.Services.Categories.Contracts;
using BookStore.Test.Tools;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookStore.Services.Test.Unit.Categories
{
    public class BookServiceTests
    {
        private readonly EFDataContext _dataContext;
        private readonly UnitOfWork _unitOfWork;
        private readonly BookService _sut;
        private readonly BookRepository _repository;
        private readonly CategoryRepository _categoryRepository;

        public BookServiceTests()
        {
            _dataContext = new EFInMemoryDatabase()
                .CreateDataContext<EFDataContext>();
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _repository = new EFBookRepository(_dataContext);
            _categoryRepository = new EFCategoryRepository(_dataContext);
            _sut = new BookAppService
                (_repository, _unitOfWork, _categoryRepository);
        }

        [Fact]
        public void Add_should_add_books_Properly()
        {
            var category = CategoryServiceTools.GenerateCategory("testTitle");
            _dataContext.Manipulate(_ => _.Categories.Add(category));
            var dto = BookServiceTools.GenerateAddBookDto(category.Id);

            _sut.Add(dto);

            _dataContext.Books.Should()
                .Contain(_ => _.Title == dto.Title);
        }

        [Fact]
        public void Add_should_throw_wrong_category_if_categoryid_is_wrong()
        {
            int fakeId = 1000;
            var dto = BookServiceTools.GenerateAddBookDto(fakeId);

            Action expected = () => _sut.Add(dto);

            expected.Should().ThrowExactly<CategoryDoesNotExistException>();
        }

        [Fact]
        public void GetAll_should_get_all_books_properly()
        {
            var categories = new List<Category>
            {
                new Category { Title = "first category"},
                new Category { Title = "second category"},
                new Category { Title = "third category"}
            };
            _dataContext.Manipulate(_ => _.Categories.AddRange(categories));
            var books = new List<Book>
            {
                new Book
                {
                    Title = "book1",
                    Author = "author1",
                    Description = "test discription",
                    CategoryId = categories[0].Id,
                    Pages = 20
                },
                new Book
                {
                    Title = "book2",
                    Author = "author2",
                    Description ="test discription",
                    CategoryId = categories[1].Id,
                    Pages = 20
                },
                new Book
                {
                    Title = "book2",
                    Author = "author2",
                    Description ="test discription",
                    CategoryId = categories[2].Id,
                    Pages = 20
                }
            };
            _dataContext.Manipulate(_ => _.Books.AddRange(books));


            var expected = _sut.GetAll();


            expected.Should().HaveCount(3);
            expected.Should().Contain(_ => _.Title == books[0].Title);
            expected.Should().Contain(_ => _.Title == books[1].Title);
            expected.Should().Contain(_ => _.Title == books[2].Title);
        }



    }


}
