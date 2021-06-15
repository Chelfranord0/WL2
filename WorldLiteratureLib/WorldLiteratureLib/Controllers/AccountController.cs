using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorldLiteratureLib.Models;
using WorldLiteratureLib.ViewModels;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WorldLiteratureLib.Controllers
{
    public class AccountController : Controller
    {
        private Data.ApplicationDbContext db;
        IWebHostEnvironment _appEnvironment;

        public AccountController(Data.ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.Include(u=>u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин или пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    var _user = new User
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNumber,
                        WebPage = model.WebPage,
                        Password = model.Password,                        
                    };

                    Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user"); //РОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИ

                    if(userRole!= null) //РОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИ
                        _user.Role = userRole; //РОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИ

                    if (model.Photo != null)
                    {

                        // путь к папке Files
                        string path = "/Files/" + model.Photo.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, System.IO.FileMode.Create))
                        {
                            await model.Photo.CopyToAsync(fileStream);
                        }
                        _user.Photo = path;
                    }
                    // добавляем пользователя в бд
                    db.Users.Add(_user);
                    await db.SaveChangesAsync();

                    await Authenticate(_user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name) //РОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИРОЛИ
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
