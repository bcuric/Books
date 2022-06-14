using Books.Data;
using Books.Models;
using Books.Entities;
using Books.Helpers;

namespace Books.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public MathodResult<string> Borrow(BookDto dto)
        {
            try
            {
                var book = bookRepository.GetById(dto.Id);
                if (book == null)
                {
                    return new MathodResult<string> { IsValid = false, Message = "Not found" };
                }
                if (!string.IsNullOrEmpty(book.Borrower))
                {
                    return new MathodResult<string> { IsValid = false, Message = "Book already borrowed" };
                }
                book.Borrower = dto.Borrower;
                bookRepository.Update(book);
                return new MathodResult<string> { IsValid = true };
            }
            catch (Exception ex)
            {
                return new MathodResult<string> { IsValid = false, Message = ex.Message };
            }
        }

        public MathodResult<string> Create(BookDto dto)
        {
            try
            {
                var id = Guid.NewGuid();
                bookRepository.Create(new Book
                {
                    Id = id,
                    Author = dto.Author,
                    Borrower = dto.Borrower,
                    Description = dto.Description,
                    PublishDate = dto.PublishDate,
                    Genre = dto.Genre,
                    Price = dto.Price,
                    Title = dto.Title
                });
                return new MathodResult<string> { IsValid = true };
            }
            catch (Exception ex)
            {
                return new MathodResult<string> { IsValid = false, Message = ex.Message };
            }
        }

        public MathodResult<string> Delete(Guid id)
        {
            try
            {
                var book = bookRepository.GetById(id);
                if (book == null)
                {
                    return new MathodResult<string> { IsValid = false, Message = "Not found" };
                }
                bookRepository.Delete(id);
                return new MathodResult<string> { IsValid = true };
            }
            catch (Exception ex)
            {
                return new MathodResult<string> { IsValid = false, Message = ex.Message };
            }
        }

        public MathodResult<BookDto> Get(Guid id)
        {
            try
            {
                var book = bookRepository.GetById(id);
                if (book == null)
                {
                    return new MathodResult<BookDto> { IsValid = false, Message = "Not found" };
                }
                var bookDto = new BookDto
                {
                    Id = id,
                    Author = book.Author,
                    Borrower = book.Borrower,
                    Description = book.Description,
                    PublishDate = book.PublishDate,
                    Genre = book.Genre,
                    Price = book.Price,
                    Title = book.Title
                };
                return new MathodResult<BookDto> { IsValid = true, Data = bookDto };
            }
            catch (Exception ex)
            {
                return new MathodResult<BookDto> { IsValid = false, Message = ex.Message };
            }
        }

        public MathodResult<List<BookDto>> GetAll()
        {
            try
            {
                var books = bookRepository.GetAll();
                var bookDtos = new List<BookDto>();
                foreach (var book in books)
                {
                    bookDtos.Add(new BookDto
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Title = book.Title,
                        Genre = book.Genre,
                        Description = book.Description,
                        Price = book.Price,
                        PublishDate = book.PublishDate,
                        Borrower = book.Borrower
                    });
                };
                return new MathodResult<List<BookDto>> { IsValid = true, Data = bookDtos };
            }
            catch (Exception ex)
            {
                return new MathodResult<List<BookDto>> { IsValid = false, Message = ex.Message };
            }
        }

        public MathodResult<string> Return(Guid id)
        {
            try
            {
                var book = bookRepository.GetById(id);
                if (book == null)
                {
                    return new MathodResult<string> { IsValid = false, Message = "Not found" };
                }
                if (string.IsNullOrEmpty(book.Borrower))
                {
                    return new MathodResult<string> { IsValid = false, Message = "Book is not borrowed" };
                }
                book.Borrower = null;
                bookRepository.Update(book);
                return new MathodResult<string> { IsValid = true };
            }
            catch (Exception ex)
            {
                return new MathodResult<string> { IsValid = false, Message = ex.Message };
            }
        }

        public MathodResult<string> Update(BookDto dto)
        {
            try
            {
                var book = bookRepository.GetById(dto.Id);
                if (book == null)
                {
                    return new MathodResult<string> { IsValid = false, Message = "Not found" };
                }
                book.Author = dto.Author;
                book.Borrower = dto.Borrower;
                book.Price = dto.Price;
                book.Description = dto.Description;
                book.PublishDate = dto.PublishDate;
                book.Genre = dto.Genre;
                book.Title = dto.Title;
                bookRepository.Update(book);
                return new MathodResult<string> { IsValid = true };
            }
            catch (Exception ex)
            {
                return new MathodResult<string> { IsValid = false, Message = ex.Message };
            }
        }
    }
}
