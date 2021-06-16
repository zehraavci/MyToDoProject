using System;
using System.ComponentModel.DataAnnotations;

namespace MyToDoProject.Models
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge = 18;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_minimumAge) < DateTime.Now;
            }

            return false;
        }
    }
}