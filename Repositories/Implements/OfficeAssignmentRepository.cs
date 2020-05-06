using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repositories.Implements
{
    public class OfficeAssignmentRepository : GenericRepository<OfficeAssignment>, IOfficeAssignmentRepository
    {
        private SchoolContext _schoolContext;
        public OfficeAssignmentRepository(SchoolContext SchoolContext) : base(SchoolContext)
        {
            _schoolContext = SchoolContext;
        }

        public new async Task<List<OfficeAssignment>> GetAll()
        {
            var listOfficeAssignment = await _schoolContext.OfficeAssignments
                .Include(x => x.Instructor)
                .ToListAsync();

            return listOfficeAssignment;
        }
    }
}
