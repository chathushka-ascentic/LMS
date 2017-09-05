using Plug.DataRepositories;
using System.Threading.Tasks;

namespace Plug.Data
{
    public class PlugUnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly IDbContextFactory _contextFactory;

        private PlugDbContext _context;

        #endregion

        #region Properties

        protected PlugDbContext Context => _context ?? (_context = _contextFactory.Get());

        public ICourseRepository Courses { get; }

        public IStudentRepository Students { get; }

        public IEnrollmentRepository Enrollments { get; }

        #endregion

        #region Constructor

        public PlugUnitOfWork(IDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;

            Courses = new CourseRepository(Context);
            Students = new StudentRepository(Context);
            Enrollments = new EnrollmentRepository(Context);
        }

        #endregion

        #region Public Methods

        public async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }

        #endregion
    }
}
