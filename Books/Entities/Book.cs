namespace Books.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Description { get; set; }
        public string? Borrower { get; set; }
    }
}
