using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorldLiteratureLib.Models;
using WorldLiteratureLib.ViewModels;

namespace WorldLiteratureLib.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Data.ApplicationDbContext db;
        public HomeController(Data.ApplicationDbContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;
        }

        [Authorize(Roles = "admin, user")]
        public IActionResult Index()
        {
            var model = new BookViewModel();
            model.Books = db.Books.ToList();
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            //return Content($"Ваша роль{role}");

            return View(model);
        }

        [Authorize(Roles ="admin")]
        public IActionResult UserList()
        {
            var model = new UserViewModel();
            model.Users = db.Users.ToList(); //model.Users = db.Users.Where(X => X.Email == "" )            
            return View(model);

            //return Content("Вход только для администратора");
        }

        [HttpGet]
        [Authorize(Roles ="admin, user")]
        public IActionResult UserPaige(User model)
        {
            User user = model;
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public IActionResult About()
        {
            //return View();
            return Content("Вход только для администратора");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
