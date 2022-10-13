using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IBookService
    {
        Task<Book> GetBookByIdAsync(Guid bookId, bool trackChanges);
        Task<List<Book>> GetAllBooksAsync(bool trackChanges);
        Task CreateBookAsync(BookForCreationDto book);
        Task UpdateBookAsync(Guid bookId, BookForUpdateDto bookForUpdate);
        Task DeleteBookAsync(Guid bookId);
    }
}
