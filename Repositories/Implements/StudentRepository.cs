using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repositories.Implements
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private SchoolContext _schoolContext;
        public StudentRepository(SchoolContext SchoolContext) : base(SchoolContext) 
        {
            _schoolContext = SchoolContext;
        }

        public async Task<IEnumerable<Course>> GetCoursesByStudent(int id)
        {
            var listCourses = await (from enrollment in _schoolContext.Enrollments
                                      join course in _schoolContext.Courses on enrollment.CourseID equals course.CourseID
                                      where enrollment.StudentID == id
                                      select course).ToListAsync();

            return listCourses;
            //prueba
        }
    }    
}
