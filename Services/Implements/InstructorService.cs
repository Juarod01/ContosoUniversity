using ContosoUniversity.Models;
using ContosoUniversity.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Services.Implements
{
    public class InstructorService : GenericService<Instructor>, IInstructorService
    {
        private IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository) : base(instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
    }
}
