using Microsoft.EntityFrameworkCore;
using Plug.DataRepositories;
using Plug.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plug.Data
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        #region Implmentation 

        public CourseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get Course with choises by id
        /// </summary>
        /// <param name="courseId">course id</param>
        /// <returns>Course</returns>
        public Course GetCourseWithChoices(Guid courseId)
        {
            IQueryable<Course> query = DbSet;

            var course = query.Where(c => c.Id == courseId).Include(e => e.Modules).FirstOrDefault();

            var baseIds = course.Modules.OfType<Question>().Select(b => b.Id);

            Context.Set<Module>()
                .OfType<Question>()
                .Include(e => e.Choices)
                .Where(e => baseIds.Contains(e.Id))
                .Load();

            Context.Entry(course).State = EntityState.Detached;

            foreach (var module in course.Modules)
            {
                Context.Entry(module).State = EntityState.Detached;

                if (module is Question)
                {
                    foreach (var choice in (module as Question).Choices)
                    {
                        Context.Entry(choice).State = EntityState.Detached;
                    }
                }
            }

            return course;
        }

        /// <summary>
        /// Get course without choices by id
        /// </summary>
        /// <param name="courseId">course id</param>
        /// <returns>Course</returns>
        public Course GetCourse(Guid courseId)
        {
            IQueryable<Course> query = DbSet;

            var course = query.Where(c => c.Id == courseId).Include(e => e.Modules).AsNoTracking().FirstOrDefault();

            return course;
        }

        /// <summary>
        /// Get collection of courses with choices
        /// </summary>
        /// <returns>collection of courses</returns>
        public IEnumerable<Course> GetCoursesWithChoices()
        {
            IQueryable<Course> query = DbSet;

            var courses = query.Include(e => e.Modules).ToList();

            var moduleIds = courses.SelectMany(e => e.Modules.OfType<Question>().Select(b => b.Id));

            Context.Set<Module>()
                .OfType<Question>()
                .Include(e => e.Choices)
                .Where(e => moduleIds.Contains(e.Id))
                .Load();

            foreach (var course in courses)
            {
                Context.Entry(course).State = EntityState.Detached;

                foreach (var module in course.Modules)
                {
                    Context.Entry(module).State = EntityState.Detached;

                    if (module is Question)
                    {
                        foreach (var choice in (module as Question).Choices)
                        {
                            Context.Entry(choice).State = EntityState.Detached;
                        }
                    }
                }
            }

            return courses;
        }

        /// <summary>
        /// Get collection of courses with out choices
        /// </summary>
        /// <returns>collection of courses</returns>
        public IEnumerable<Course> GetCourses()
        {
            IQueryable<Course> query = DbSet;

            var courses = query.Include(e => e.Modules).AsNoTracking().ToList();

            return courses;
        }

        #endregion
    }
}
