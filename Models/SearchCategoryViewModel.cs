using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoProject.Models
{
    public class SearchCategoryViewModel
    {
        public string SearchTitle { get; set; }

        public bool InDescription { get; set; }

        public List<Category> Result { get; set; }
    }
}
