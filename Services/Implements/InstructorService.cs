using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.Repositories;

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
