using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context) {}
        public async Task<Book> GetBookByIdAsync(Guid bookId, bool trackChanges) =>
            await FindByCondition(b => b.Id == bookId, false).FirstOrDefaultAsync();
        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public void CreateBook(Book book) => Create(book);
        public void UpdateBook(Book book) => Update(book);
        public void DeleteBook(Book book) => Delete(book);
    }
}
