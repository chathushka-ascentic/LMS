using Plug.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plug.Services
{
    public interface IEnrollmentService
    {
        /// <summary>
        /// Add an enrollment
        /// </summary>
        /// <param name="input">Object of Enrollment Model</param>
        /// <returns>Result of Adding an enrollment</returns>
        Task<OutputResult> AddEnrollment(Input<EnrollmentModel> input);

        /// <summary>
        /// Get an enrollment by enrollment id
        /// </summary>
        /// <param name="input">Enrollment id</param>
        /// <returns>Object of Enrollment Model</returns>
        Output<EnrollmentModel> GetEnrollment(Input<Guid> input);

        /// <summary>
        /// Get all enrollments by student id
        /// </summary>
        /// <param name="input">Student id</param>
        /// <returns>List of Enrollments</returns>
        Output<IList<EnrollmentModel>> GetEnrollments(Input<Guid> input);

        /// <summary>
        /// Update Enrolled Module
        /// </summary>
        /// <param name="input">Object of Enrollem Module Model</param>
        /// <returns>Result of Update an enrolled module</returns>
        Task<OutputResult> UpdateEnrolledModule(Input<EnrollModuleModel> input);

        /// <summary>
        /// Complete Enrolled Module
        /// </summary>
        /// <param name="input">Object of Enrollem Module Model</param>
        /// <returns>Result of Update an enrolled module</returns>
        Task<OutputResult> CompleteEnrolledModule(Input<EnrollModuleModel> input);
    }
}
