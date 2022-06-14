using Books.Entities;
using System.Xml;

namespace Books.Services.Implementation
{
    public class InitialLoadXml : IInitaialDataLoad
    {
        private readonly IBookService bookService;

        public InitialLoadXml(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public void InitaialData()
        {
            var books = new List<Book>();
            string path = "books.xml";
            XmlDocument xmlDocument = new();
            using (var fs = new FileStream(path, FileMode.Open))
            {
                xmlDocument.Load(fs);
                var xmlElement = xmlDocument.DocumentElement;
                var catalogNodeList = new List<XmlNode>(xmlElement!.ChildNodes.Cast<XmlNode>());
                foreach (XmlNode bookNode in catalogNodeList)
                {
                    bookService.Create(new Models.BookDto
                    {
                        Id = Guid.NewGuid(),
                        Title = bookNode.SelectSingleNode("title")!.InnerText,
                        Description = bookNode.SelectSingleNode("description")!.InnerText,
                        Price = decimal.Parse(bookNode.SelectSingleNode("price")!.InnerText),
                        PublishDate = DateTime.Parse(bookNode.SelectSingleNode("publish_date")!.InnerText),
                        Author = bookNode.SelectSingleNode("author")!.InnerText,
                        Genre = bookNode.SelectSingleNode("genre")!.InnerText,
                        Borrower = ""
                    }); 
                }
            }
        }
    }
}
