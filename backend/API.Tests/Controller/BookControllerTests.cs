using API.Controllers;
using Entities.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Controller
{
    public class BookControllerTests
    {
        private readonly IServiceManager _service;
        private readonly BookController _controller;
        public BookControllerTests()
        {
            _service = A.Fake<IServiceManager>();
            _controller = new BookController(_service);
        }
        [Fact]
        public async Task BookController_GetAllBooks_ReturnsOk()
        {
            // Arrange
            var booksList = new List<Book>();
            A.CallTo(() => _service.BookService.GetAllBooksAsync(false))
                .Returns(Task.FromResult(booksList));

            // Act
            var result = await _controller.GetAllBooks();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task BookController_GetBook_ReturnsOk()
        {
            // Arrange
            var id = new Guid();
            var book = new Book();
            A.CallTo(() => _service.BookService.GetBookByIdAsync(id, false))
                .Returns(Task.FromResult(book));

            // Act
            var result = await _controller.GetBook(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public async Task BookController_CreateBook_ReturnsOk()
        {
            // Arrange
            var bookForCreation = new BookForCreationDto();
            A.CallTo(() => _service.BookService.CreateBookAsync(bookForCreation))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateBook(bookForCreation);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Fact]
        public async Task BookController_UpdateBook_ReturnsOk()
        {
            // Arrange
            var id = new Guid();
            var bookForUpdate = new BookForUpdateDto();
            A.CallTo(() => _service.BookService.UpdateBookAsync(id, bookForUpdate))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateBook(id, bookForUpdate);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Fact]
        public async Task BookController_DeleteBook_ReturnsOk()
        {
            // Arrange
            var id = new Guid();
            A.CallTo(() => _service.BookService.DeleteBookAsync(id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBook(id);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
