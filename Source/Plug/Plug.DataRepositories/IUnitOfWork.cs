using System.Threading.Tasks;

namespace Plug.DataRepositories
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        IEnrollmentRepository Enrollments { get; }
        Task<int> Commit();
    }
}
