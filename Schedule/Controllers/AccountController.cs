using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Schedule.Models;
using Schedule.ViewModels;
using Schedule.Data;
using System.Linq;

namespace Schedule.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ApplicationContext db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,ApplicationContext _db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = _db;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // установка массива байтов model.Password, 
            if (ModelState.IsValid)
            {
                var q = db.Users.FirstOrDefault(x => x.Faculty == model.Faculty && x.EducationalInstitutions == model.EducationalInstitutions);
                if (q == null)
                {
                    User user = new User { Email = model.Email,UserName = model.Email,FIO=model.FIO,EducationalInstitutions=model.EducationalInstitutions,Faculty=model.Faculty};
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                }
                else
                {
                    return NotFound("Такой аккаунт уже зарегистрирован");
                }
            }
            
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}