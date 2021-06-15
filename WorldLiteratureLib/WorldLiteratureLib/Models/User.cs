using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorldLiteratureLib.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string WebPage { get; set; }
        public bool BlackList { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int? Role_Id { get; set; }
        public Role Role { get; set; }
    }

}
