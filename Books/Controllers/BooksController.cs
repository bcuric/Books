using Microsoft.AspNetCore.Mvc;
using Books.Models;
using Books.Services;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var result = bookService.GetAll();
            if (!result.IsValid)
            {
                return NotFound();
            }
            return View(result.Data);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var result = bookService.Get(id);
            if(!result.IsValid)
            {
                return NotFound();
            }
            return View(result.Data);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Title,Genre,Price,PublishDate,Description,Borrower")] BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                bookService.Create(bookDto);
                return RedirectToAction(nameof(Index));
            }
            return View(bookDto);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = bookService.Get(id);
            if (!result.IsValid)
            {
                return NotFound();
            }
            return View(result.Data);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Author,Title,Genre,Price,PublishDate,Description,Borrower")] BookDto bookDto)
        {
            var result = bookService.Update(bookDto);
            if (!result.IsValid)
            {
                return View(bookDto);
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = bookService.Delete(id);
            if (!result.IsValid)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Borrow/5
        public async Task<IActionResult> Borrow(Guid id)
        {
            var result = bookService.Get(id);
            if (!result.IsValid)
            {
                return NotFound();
            }
            return View(result.Data);
        }

        // POST: Books/Borrow/5
        [HttpPost, ActionName("Borrow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(BookDto bookDto)
        {
            var result = bookService.Borrow(bookDto);
            if (!result.IsValid)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Books/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(Guid id)
        {
            var result = bookService.Return(id);
            if (!result.IsValid)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
