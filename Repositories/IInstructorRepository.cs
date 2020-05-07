using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Repositories
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        Task<IEnumerable<Course>> GetCoursesByInstructor(int id);
        Task<IEnumerable<Enrollment>> GetStudentsByCourse(int id);
    }
}