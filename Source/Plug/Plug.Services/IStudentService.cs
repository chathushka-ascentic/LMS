using Plug.Models;

namespace Plug.Services
{
    public interface IStudentService
    {
        /// <summary>
        /// Verify Student Login
        /// </summary>
        /// <param name="input">Object Student Model</param>
        /// <returns>Result of verification</returns>
        Output<StudentModel> VerifyStudent(Input<StudentModel> input);
    }
}
