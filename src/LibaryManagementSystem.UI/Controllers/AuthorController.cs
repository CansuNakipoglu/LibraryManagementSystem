using LibaryManagementSystem.Core.Models.Entities;
using LibaryManagementSystem.Data.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LibaryManagementSystem.UI.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IGenericRepository<Author> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _toastNotification;

        public AuthorController(IGenericRepository<Author> repository, IUnitOfWork unitOfWork, IToastNotification toastNotification)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var authors = _repository.GetAll();

            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (author == null) return BadRequest();

            _repository.Add(author);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage(author.FirstName + " " + author.LastName  + " İsimli yazarın bilgilerini sisteme eklediniz", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0) return BadRequest();
            var author = await _repository.GetAsync(id);

            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (author == null) return BadRequest();

            _repository.Update(author);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage(author.FirstName + " " + author.LastName + " İsimli yazarın bilgileri güncellendi", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0) return BadRequest();
            var author = await _repository.GetAsync(id);

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            var author = await _repository.GetAsync(id);

            _repository.Delete(author);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage(author.FirstName + " " + author.LastName + " İsimli yazarın biglileri silindi", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("index");
        }
    }
}
