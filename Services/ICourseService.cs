using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Services
{
    public interface ICourseService : IGenericService<Course>
    {
        Task<IEnumerable<Student>> GetStudentsByCourse(int id);
    }
}
