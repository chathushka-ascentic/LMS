using Microsoft.EntityFrameworkCore;
using Plug.DataRepositories;
using Plug.Entities;
using System;
using System.Linq;

namespace Plug.Data
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        #region Implmentation 

        public EnrollmentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get Enroll Modules
        /// </summary>
        /// <param name="moduleId">module Id</param>
        /// <param name="enrolledId">enrolled Id</param>
        /// <returns>Object of Enroll</returns>
        public EnrollModule GetEnrollModule(Guid moduleId, Guid enrolledId)
        {
            IQueryable<Enrollment> query = DbSet;

            var enrollModule = query.Include(e => e.EnrollModules)
                .FirstOrDefault(e => e.Id == enrolledId)
                ?.EnrollModules
                .FirstOrDefault(em => em.EnrollmentId == enrolledId && em.ModuleId == moduleId);

            return enrollModule;
        }

        #endregion
    }
}
