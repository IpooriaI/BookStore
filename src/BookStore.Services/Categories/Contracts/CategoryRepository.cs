using BookStore.Entities;
using BookStore.Infrastructure.Application;
using System.Collections.Generic;

namespace BookStore.Services.Categories.Contracts
{
    public interface CategoryRepository : Repository
    {
        void Add(Category category);
        Category GetById(int id);
        IList<GetCategoryDto> GetAll();
        void Delete(Category category);
        bool IsCategoryTitleExist(string title);
    }
}
