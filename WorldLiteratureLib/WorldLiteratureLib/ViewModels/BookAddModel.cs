using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorldLiteratureLib.Models;

namespace WorldLiteratureLib.ViewModels
{
    [Authorize(Roles = "admin")]
    public class BookAddModel
    {        
        [Required(ErrorMessage ="Введите название")]
        public string BookName { get; set; }
        [Required(ErrorMessage ="Заполните описание")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Введите имя автора")]
        public string Author { get; set; }
        [Required(ErrorMessage ="Сколько поступило на склад")]
        public int InStock { get; set; }
    }
}
