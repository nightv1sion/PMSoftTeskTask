using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(Guid bookId, bool trackChanges);
        Task<List<Book>> GetAllBooksAsync(bool trackChanges);
        void CreateBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(Book book);

    }
}
