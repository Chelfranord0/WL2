using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldLiteratureLib.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int InStock { get; set; }
        public string Image_way { get; set; }
    }
}
