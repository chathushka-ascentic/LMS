using Plug.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plug.Services
{
    public interface ICourseService
    {
        /// <summary>
        /// Add a course 
        /// </summary>
        /// <param name="input">Object of Course Model</param>
        /// <returns>Result of adding a course</returns>
        Task<OutputResult> AddCourse(Input<CourseModel> input);

        /// <summary>
        /// Update a course
        /// </summary>
        /// <param name="input">Object of Course Model</param>
        /// <returns>Result of update a course</returns>
        Task<OutputResult> UpdateCourse(Input<CourseModel> input);

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <param name="input">Student Id to check the enrollment</param>
        /// <returns>List of Course Models</returns>
        Output<IList<CourseModel>> GetCourses(Input<Guid> input);

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns>List of Course Models</returns>
        Output<IList<CourseModel>> GetCourses();

        /// <summary>
        /// Get Course by Course Id
        /// </summary>
        /// <param name="input">Course Id</param>
        /// <returns>Object of Course</returns>
        Output<CourseModel> GetCourse(Input<Guid> input);

        /// <summary>
        /// Get Module by Course Id and Module Id
        /// </summary>
        /// <param name="input">Course Id and Module Id as Key Value Pair</param>
        /// <returns>Object of Module Model</returns>
        Output<ModuleModel> GetModule(Input<KeyValuePair<Guid, Guid>> input);
    }
}
