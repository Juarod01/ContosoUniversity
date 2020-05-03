using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repositories.Implements
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        private SchoolContext _schoolContext;
        public EnrollmentRepository(SchoolContext schoolContext) : base(schoolContext)
        {
            _schoolContext = schoolContext;
        }
        public new async Task<List<Enrollment>> GetAll()
        {
            var listEnrollment = await _schoolContext.Enrollments
                .Include(x => x.Course)
                .Include(x => x.Student)
                .ToListAsync();

            //var listEnrollments = await (from enrollments in schoolContext.Enrollments
//                      join course in schoolContext.Courses on enrollments.CourseID equals course.CourseID
//                      join student in schoolContext.Students on enrollments.StudentID equals student.ID
//                      select enrollments).ToListAsync();

            return listEnrollment;
        }
    }
}
