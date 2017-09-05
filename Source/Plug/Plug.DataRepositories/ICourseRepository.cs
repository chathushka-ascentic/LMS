using Plug.Entities;
using System;
using System.Collections.Generic;

namespace Plug.DataRepositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetCoursesWithChoices();

        IEnumerable<Course> GetCourses();

        Course GetCourseWithChoices(Guid courseId);

        Course GetCourse(Guid courseId);
    }
}
