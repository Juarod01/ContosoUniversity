using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Services
{
    public interface IInstructorService : IGenericService<Instructor>
    {
        Task<IEnumerable<Course>> GetCoursesByInstructor(int id);
        Task<IEnumerable<Enrollment>> GetStudentsByCourse(int id);
    }
}
