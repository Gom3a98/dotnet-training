using BookStore.Models.Repositories;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;

        public BookController(IBookStoreRepository<Book> bookRepository , IBookStoreRepository<Author>authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.GetAll();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.GetById(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                authors = authorRepository.GetAll().ToList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel bookAuthor)
        {
            try
            {
                var author = authorRepository.GetById(bookAuthor.AuthorId);
                Book book = new Book
                {
                    Id = bookAuthor.BookId,
                    Author = author,
                    Title = bookAuthor.Title,
                    Description = bookAuthor.Description
                };
                bookRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book  =  bookRepository.GetById(id);
            var authorId = book.Author == null ? 0 : book.Author.Id;
            var bookViewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                authors = authorRepository.GetAll().ToList()
            };
            return View(bookViewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel bookModel)
        {
            try
            {
                var author = authorRepository.GetById(bookModel.AuthorId);
                Book book = new Book
                {
                    Author = author,
                    Title = bookModel.Title,
                    Description = bookModel.Description
                };
                bookRepository.Update(bookModel.BookId , book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            bookRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
