using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoProject.Models
{
    public class SearchViewModel
    {
        public string SearchTitle { get; set; }
        public bool InDescription { get; set; }
        public int CategoryId { get; set; }

        public List<ToDo> Result { get; set; }
    }
}
