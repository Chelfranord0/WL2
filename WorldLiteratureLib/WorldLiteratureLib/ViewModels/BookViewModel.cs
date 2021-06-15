using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldLiteratureLib.Models;
using WorldLiteratureLib.Data;
using Microsoft.EntityFrameworkCore;


namespace WorldLiteratureLib.ViewModels
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
    }
}
