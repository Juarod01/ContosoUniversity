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
    }
}
