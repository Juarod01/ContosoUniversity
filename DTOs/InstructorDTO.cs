using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.DTOs
{
    public class InstructorDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The First MidName is required")]
        [Display(Name = "First MidName")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "The Hire Data is required")]
        [Display(Name = "Hire Data")]
        public DateTime HireDate { get; set; }
    }
}
