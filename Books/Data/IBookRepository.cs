using Books.Entities;

namespace Books.Data
{
    public interface IBookRepository
    {
        public List<Book> GetAll();
        public Book GetById(Guid id);
        public Book Create(Book book);
        public Book Update(Book book);
        public void Delete(Guid id);
    }

    public class BookRepository : IBookRepository
    {
        private readonly BooksContext context;
        public BookRepository(BooksContext context)
        {
            this.context = context;
        }

        public List<Book> GetAll() 
        { 
            return context.Books.ToList();
        }

        public Book GetById(Guid id)
        {
            return context.Books.First(x => x.Id == id);
        }

        public Book Create(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }

        public Book Update(Book book)
        {
            var dbBook = context.Books.FirstOrDefault(x => x.Id == book.Id);
            dbBook.Author = book.Author;
            dbBook.Borrower = book.Borrower;
            dbBook.Price = book.Price;
            dbBook.Description = book.Description;
            dbBook.PublishDate = book.PublishDate;
            dbBook.Genre = book.Genre;
            dbBook.Title = book.Title;
            context.SaveChanges();
            return book;
        }

        public void Delete(Guid id)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == id);
            context.Books.Remove(book);
            context.SaveChanges();
        }
    }
}
