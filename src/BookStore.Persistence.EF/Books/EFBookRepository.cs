using BookStore.Entities;
using BookStore.Services.Books.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Persistence.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _dataContext;
        public EFBookRepository(EFDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Book book)
        {
            _dataContext.Books.Add(book);
        }

        public List<GetBookDto> GetAll()
        {
            return _dataContext.Books.Select(_ => new GetBookDto
            {
                Title = _.Title,
                Pages = _.Pages,
                Author = _.Author,
                CategoryId = _.CategoryId,
                Description = _.Description,
            }).ToList();
        }
    }
}
