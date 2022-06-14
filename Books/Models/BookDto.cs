using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public string Borrower { get; set; }

    }
}
