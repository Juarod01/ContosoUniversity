using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContosoUniversity.Models;

namespace ContosoUniversity.DTOs
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class EnrollmentDTO
    {
        public int EnrollmentID { get; set; }

        [Required(ErrorMessage = "The Course Id is required")]
        [Display(Name = "Course Id")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "The Student Id is required")]
        [Display(Name = "Student Id")]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "The Grade is required")]
        [Display(Name = "Grade")]
        public Grade? Grade { get; set; }


        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
