using Plug.Entities;
using System;

namespace Plug.DataRepositories
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        EnrollModule GetEnrollModule(Guid moduleId, Guid enrolledId);
    }
}
