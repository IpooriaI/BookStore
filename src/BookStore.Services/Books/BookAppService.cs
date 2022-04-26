using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Books.Contracts;
using BookStore.Services.Books.Exceptions;
using BookStore.Services.Categories.Contracts;
using System.Collections.Generic;

namespace BookStore.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _repository;
        private readonly CategoryRepository _categoryRepository;
        private readonly UnitOfWork _unitOfWork;

        public BookAppService(
            BookRepository repository,
            UnitOfWork unitOfWork,
            CategoryRepository categoryRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public void Add(AddBookDto dto)
        {
            var book = new Book
            {
                Description = dto.Description,
                Author = dto.Author,
                Pages = dto.Pages,
                Title = dto.Title,
                CategoryId = dto.CategoryId,
            };

            var category = _categoryRepository.GetById(dto.CategoryId);

            if (category == null)
            {
                throw new CategoryDoesNotExistException();
            }


            _repository.Add(book);
            _unitOfWork.Commit();
        }

        public List<GetBookDto> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
