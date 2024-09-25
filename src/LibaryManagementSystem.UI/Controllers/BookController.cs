using LibaryManagementSystem.Core.Models.Entities;
using LibaryManagementSystem.Data.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Net;

namespace LibaryManagementSystem.UI.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;

        public BookController(IGenericRepository<Book> repository, IUnitOfWork unitOfWork, IToastNotification toastNotification, IGenericRepository<Author> authorRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _repository.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = _authorRepository.GetAll();
            ViewBag.Authors = new SelectList(authors, "Id", "FirstName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (book == null) return BadRequest();

            _repository.Add(book);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage(book.Title + " Kitabını sisteme eklediniz", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0) return BadRequest();
            var book = await _repository.GetAsync(id);

            var authors = _authorRepository.GetAll();
            ViewBag.Authors = new SelectList(authors, "Id", "FirstName");

            return View(book);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            if (book == null) return BadRequest();

            _repository.Update(book);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage(book.Title + " Kitabı güncellendi", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return BadRequest();
            var book = await _repository.GetAsync(id);

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            var book = await _repository.GetAsync(id);

            _repository.Delete(book);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage(book.Title + " Kitabı Silindi", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("index");
        }
    }
}
