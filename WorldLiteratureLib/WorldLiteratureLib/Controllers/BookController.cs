using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldLiteratureLib.Models;
using WorldLiteratureLib.ViewModels;

namespace WorldLiteratureLib.Controllers
{
    public class BookController : Controller
    {
        private Data.ApplicationDbContext db;
        public BookController(Data.ApplicationDbContext context)
        {
            db = context;            
        }   
        
       
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult BookAdding()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BookAdding (BookAddModel model)
        {
            if (ModelState.IsValid)
            {
                Book book = await db.Books.FirstOrDefaultAsync(book => book.BookName == model.BookName);
                if (book == null)
                {
                    db.Books.Add(new Book
                    {
                        BookName    = model.BookName,
                        Author      = model.Author,
                        Description = model.Description,
                        InStock     = model.InStock
                    });
                    await db.SaveChangesAsync();                    
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View(model);
        }
    }
}
