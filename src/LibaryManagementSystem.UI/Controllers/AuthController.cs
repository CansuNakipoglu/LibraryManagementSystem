using LibaryManagementSystem.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
using LibaryManagementSystem.Core.Models.Entities;
using LibaryManagementSystem.Data.Repositories.Abstracts;

namespace LibaryManagementSystem.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IGenericRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IToastNotification toastNotification, IGenericRepository<User> repository, IUnitOfWork unitOfWork)
        {
            _toastNotification = toastNotification;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login(int isError)
        {
            if (isError == 1) 
            {
                _toastNotification.AddErrorToastMessage("Bu Sayfayı Görüntüleyebilmek İçin Lütfen Giriş Yapın", new ToastrOptions { Title = "Hata" });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserVM model)
        {
            var user = _repository.FindBy(x => x.Email == model.Email).FirstOrDefault();
            
            if(user == null)
            {
                _toastNotification.AddErrorToastMessage("Geçersiz kullanıcı adı veya şifre.", new ToastrOptions { Title = "Hata" });
                return View();
            }

            if (user.Password == model.Password)
            {
                var claims = new List<Claim>{ new Claim(ClaimTypes.Name, model.Email) };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties);

                _toastNotification.AddSuccessToastMessage("Giriş Yaptınız.", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index", "Home");
            }

            _toastNotification.AddErrorToastMessage("Geçersiz kullanıcı adı veya şifre.", new ToastrOptions { Title = "Hata" });
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserVM model)
        {
            if (model.Password != model.RePassword)
            {
                _toastNotification.AddErrorToastMessage("Şifre tekrarını yanlış girdiniz", new ToastrOptions { Title = "Hata" });

                return View();
            }

            var user = new User 
            {
                Email = model.Email,
                FullName = model.FullName,
                JoinDate = DateTime.Now,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            };

            _repository.Add(user);
            _unitOfWork.Commit();

            _toastNotification.AddSuccessToastMessage("Kayıt Olundu", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Login", "Auth");
        }
    }
}
