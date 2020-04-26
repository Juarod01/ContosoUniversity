using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Services
{
    public interface IStudentService : IGenericService<Student>
    {
        Task<IEnumerable<Course>> GetCoursesByStudent(int id);
    }
}
