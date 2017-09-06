using Microsoft.EntityFrameworkCore;
using Plug.DataRepositories;
using Plug.Entities;

namespace Plug.Data
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
