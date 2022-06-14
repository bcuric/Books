using Books.Helpers;
using Books.Models;

namespace Books.Services
{
    public interface IBookService
    {
        public MathodResult<List<BookDto>> GetAll();
        public MathodResult<BookDto> Get(Guid id);
        public MathodResult<string> Create(BookDto dto);
        public MathodResult<string> Update(BookDto dto);
        public MathodResult<string> Delete(Guid id);
        public MathodResult<string> Borrow(BookDto dto);
        public MathodResult<string> Return(Guid id);
    }
}
