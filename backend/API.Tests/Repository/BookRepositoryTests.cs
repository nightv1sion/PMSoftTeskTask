using Contracts;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Repository
{
    public class BookRepositoryTests
    {
        private readonly RepositoryContext databaseContext;
        private readonly IBookRepository bookRepository;
        public BookRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            databaseContext = new RepositoryContext(options);

            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            if (databaseContext.Books.Count() <= 0)
            {
                databaseContext.Books.AddRange(
                    new Book
                    {
                        Id = Guid.Parse("64678867-f547-49fb-8c0a-0cac1d5dc551"),
                        Name = "Eugene Onegin",
                        Author = "Alexander Pushkin",
                        Genre = "Sonnet",
                        Year = 1825
                    },

                    new Book
                    {
                        Id = Guid.Parse("dff01cd7-9b16-43a2-aad1-4d39cfbd5a1c"),
                        Name = "Crime and Punishment",
                        Author = "Fyodor Dostoevsky",
                        Genre = "Literary fiction",
                        Year = 1866
                    },

                    new Book
                    {
                        Id = Guid.Parse("58ae8f1c-8554-460a-9359-53f6f89661e4"),
                        Name = "Dead Souls",
                        Author = "Nikolai Gogol",
                        Genre = "Satire",
                        Year = 1842
                    }
                );
                databaseContext.SaveChanges();
                bookRepository = new BookRepository(databaseContext);
            }
        }
        [Fact]
        public async Task BookRepository_GetBookByIdAsync_ReturnsBook()
        {
            // Arrange
            var bookId = Guid.Parse("dff01cd7-9b16-43a2-aad1-4d39cfbd5a1c");
            var trackChanges = false;

            // Act
            var result = await bookRepository.GetBookByIdAsync(bookId, trackChanges);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Book>();
            result.Name.Should().Be("Crime and Punishment");
        }

        [Fact]
        public async Task BookRepository_GetAllBooksAsync_ReturnsList()
        {
            // Arrange
            var trackChanges = false;

            // Act
            var result = await bookRepository.GetAllBooksAsync(trackChanges);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
            result.Should().HaveCount(3);
        }
        [Fact]
        public async Task BookRepository_CreateBook_CreatesBook() 
        {
            // Arrange
            var bookId = Guid.Parse("8820a803-669a-4e71-b289-ea24fe47b1e8");
            var book = new Book()
            {
                Id = bookId,
                Name = "War and Peace",
                Author = "Leo Tolstoy",
                Genre = "Novel",
                Year = 1865
            };
            var trackChanges = false;
            // Act 
            bookRepository.CreateBook(book);
            await databaseContext.SaveChangesAsync();
            var result = await bookRepository.GetAllBooksAsync(trackChanges);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
            result.Should().HaveCount(4);
            result.Should().Contain(book => book.Id == bookId);
        }
    }
}
