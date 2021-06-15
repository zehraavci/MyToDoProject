using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoProject.Models
{
    public class CetUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual List<ToDo> ToDos { get; set; }
    }
}
