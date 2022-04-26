using BookStore.Infrastructure.Application;
using System.Collections.Generic;

namespace BookStore.Services.Books.Contracts
{
    public interface BookService : Service
    {
        void Add(AddBookDto dto);
        List<GetBookDto> GetAll();
    }
}
