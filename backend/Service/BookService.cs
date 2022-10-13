using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class BookService : IBookService
    {
        private IRepositoryManager _repository;
        private IMapper _mapper;
        public BookService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateBookAsync(BookForCreationDto bookForCreation)
        {
            var book = _mapper.Map<Book>(bookForCreation);
            _repository.Book.CreateBook(book);
            await _repository.SaveAsync();
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _repository.Book.GetAllBooksAsync(trackChanges);
            return books;
        }

        public async Task<Book> GetBookByIdAsync(Guid bookId, bool trackChanges)
        {
            var book = await GetBookAndCheckIfItExistsAsync(bookId, trackChanges);
            return book;
        }
        public async Task UpdateBookAsync(Guid bookId, BookForUpdateDto bookForUpdate)
        {
            var bookEntity = await GetBookAndCheckIfItExistsAsync(bookId, false);
            _mapper.Map<BookForUpdateDto, Book>(bookForUpdate, bookEntity);
            _repository.Book.UpdateBook(bookEntity);

            await _repository.SaveAsync();
        }

        public async Task DeleteBookAsync(Guid bookId)
        {
            var book = await GetBookAndCheckIfItExistsAsync(bookId, false);
            _repository.Book.DeleteBook(book);

            await _repository.SaveAsync();
        }
        private async Task<Book> GetBookAndCheckIfItExistsAsync(Guid bookId, bool trackChanges)
        {
            var book = await _repository.Book.GetBookByIdAsync(bookId, trackChanges);
            if (book is null)
                throw new BookBadRequstException(bookId);

            return book;
        }
    }
}
