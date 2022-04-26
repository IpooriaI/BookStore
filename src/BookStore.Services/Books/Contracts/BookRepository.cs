using BookStore.Entities;
using BookStore.Infrastructure.Application;
using System.Collections.Generic;

namespace BookStore.Services.Books.Contracts
{
    public interface BookRepository : Repository
    {
        void Add(Book book);
        List<GetBookDto> GetAll();
    }
}
