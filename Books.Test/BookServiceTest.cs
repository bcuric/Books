using Books.Data;
using Books.Entities;
using Books.Models;
using Books.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Books.Test
{
    public class BookServiceTest
    {
        private readonly BookService _bookService;
        private readonly Mock<IBookRepository> _bookRepoMock = new Mock<IBookRepository>();

        public BookServiceTest()
        {
            _bookService = new BookService(_bookRepoMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var title = "Test 1";
            var book = new Book
            { 
                Id = bookId, 
                Title = title 
            };
            _bookRepoMock.Setup(x => x.GetById(bookId)).Returns(book);

            // Act
            var result = _bookService.Get(bookId);

            // Assert
            Assert.Equal(bookId, result.Data.Id);
            Assert.Equal(title, result.Data.Title);
        }

        [Fact]
        public void Get_ShouldReturnNothing_WhenBookDoesExists()
        {
            // Arrange
            _bookRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(() => null);

            // Act
            var result = _bookService.Get(Guid.NewGuid());

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void Borrow_ShouldReturnNothing_WhenBookExistsAndIsNotAlreadyBorrowed()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var title = "Test 1";
            var borrower = "Borrower 1";
            var book = new Book
            {
                Id = bookId,
                Title = title,
                Borrower = borrower
            };
            _bookRepoMock.Setup(x => x.Update(book)).Returns(book);

            // Act
            var result = _bookService.Borrow(new BookDto
            {
                Id= bookId,
                Title= title,
                Borrower= borrower
            });

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Borrow_ShouldReturnInvalidState_WhenBookExistsAndIsAlreadyBorrowed()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var title = "Test 1";
            var borrower = "Borrower 1";
            var book = new Book
            {
                Id = bookId,
                Title = title,
                Borrower = borrower
            };
            _bookRepoMock.Setup(x => x.GetById(bookId)).Returns(book);

            // Act
            var result = _bookService.Borrow(new BookDto
            {
                Id = bookId,
                Title = title,
                Borrower = borrower
            });

            // Assert
            Assert.False(result.IsValid);
        }
    
        [Fact]
        public void Return_ShouldReturnValidState_WhenBookExistsAndIsBorrowed()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var title = "Test 1";
            var borrower = "Borrower 1";
            var book = new Book
            {
                Id = bookId,
                Title = title,
                Borrower = borrower
            };
            _bookRepoMock.Setup(x => x.GetById(bookId)).Returns(book);

            // Act
            var result = _bookService.Return(bookId);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Return_ShouldReturnInvalidState_WhenBookExistsAndIsNotBorrowed()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var title = "Test 1";
            var borrower = "Borrower 1";
            var book = new Book
            {
                Id = bookId,
                Title = title
            };
            _bookRepoMock.Setup(x => x.GetById(bookId)).Returns(book);

            // Act
            var result = _bookService.Return(bookId);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}