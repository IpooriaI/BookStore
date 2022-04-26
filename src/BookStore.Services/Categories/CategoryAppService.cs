using BookStore.Entities;
using BookStore.Infrastructure.Application;
using BookStore.Services.Categories.Contracts;
using BookStore.Services.Categories.Exceptions;
using System.Collections.Generic;

namespace BookStore.Services.Categories
{
    public class CategoryAppService : CategoryService
    {
        private readonly CategoryRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public CategoryAppService(
            CategoryRepository repository,
            UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Add(AddCategoryDto dto)
        {
            var category = new Category
            {
                Title = dto.Title,
            };

            var checkCategory = _repository
                .IsCategoryTitleExist(category.Title);
            if (checkCategory)
            {
                throw new CategoryIsAlreadyExistException();
            }


            _repository.Add(category);

            _unitOfWork.Commit();
        }

        public Category GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IList<GetCategoryDto> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(int id, UpdateCategoryDto dto)
        {
            var category = _repository.GetById(id);
            
            if (category == null)
            {
                throw new CategoryDosntExistException();
            }

            category.Title = dto.Title;
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var category = _repository.GetById(id);

            if (category == null)
            {
                throw new CategoryDosntExistException();
            }

            _repository.Delete(category);
            _unitOfWork.Commit();
        }
    }
}
