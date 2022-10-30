using API.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IServiceManager _service;
        public BookController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<ActionResult> GetAllBooks()
        {
            var books = await _service.BookService.GetAllBooksAsync(false);
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetBook(Guid id)
        {
            var book = await _service.BookService.GetBookByIdAsync(id, false);
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Policy = "ShouldBeAdmin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> CreateBook(BookForCreationDto bookForCreation)
        {
            await _service.BookService.CreateBookAsync(bookForCreation);
            return Ok();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "ShouldBeAdmin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult> UpdateBook(Guid id, BookForUpdateDto bookForUpdate)
        {
            await _service.BookService.UpdateBookAsync(id, bookForUpdate);
            return Ok();
        }

        [Authorize(Policy = "ShouldBeAdmin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            await _service.BookService.DeleteBookAsync(id);
            return Ok();
        }
    }
}
