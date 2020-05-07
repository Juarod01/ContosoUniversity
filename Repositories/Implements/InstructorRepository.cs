using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repositories.Implements
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        private SchoolContext _schoolContext;
        public InstructorRepository(SchoolContext SchoolContext) : base(SchoolContext)
        {
            _schoolContext = SchoolContext;
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructor(int id)
        {
            var listCourses = await (from courseInstructor in _schoolContext.CourseInstructors
                                     join course in _schoolContext.Courses on courseInstructor.CourseID equals course.CourseID
                                     where courseInstructor.InstructorID == id
                                     select course).ToListAsync();

            return listCourses;
        }

        public async Task<IEnumerable<Enrollment>> GetStudentsByCourse(int id)
        {
            var listStudents = await _schoolContext.Enrollments
                                     .Include(x => x.Student)
                                     .Where(x => x.CourseID == id)
                                     .ToListAsync();

            return listStudents;
        }
    }
}
