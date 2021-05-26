﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyToDoProject.Models
{
    public class ToDo
    {
        public ToDo()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Please add a title for todo")]
        [MaxLength(175)]
        public string Title { get; set; }
        [MaxLength(1750)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CompletedDate { get; set; }
        public int RemainingHour
        {
            get
            {
                var remainingTime = (DateTime.Now - DueDate);
                return (int)remainingTime.TotalHours;
            }
        }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
