using Plug.DataRepositories;
using Plug.Models;
using Plug.Resources;
using Plug.Services;
using System;

namespace Plug.Business
{
    public class StudentService : ServiceBase, IStudentService
    {
        /// <summary>
        /// Constructor of the class Student Service
        /// </summary>
        /// <param name="unitOfWork">Unit of Work</param>
        public StudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Verify Student Login
        /// </summary>
        /// <param name="input">Object Student Model</param>
        /// <returns>Result of verification</returns>
        public Output<StudentModel> VerifyStudent(Input<StudentModel> input)
        {
            try
            {
                // Validate Input
                if (!input.Validate)
                {
                    return Output<StudentModel>.FailOutput(Constant.InvalidInput);
                }

                // Retrive Entities
                var student = UnitOfWork.Students.FirstOrDefault(s => s.Email == input.Arguments.Email & s.Password == input.Arguments.Password);

                // Validate Entities
                if (student == null)
                {
                    return Output<StudentModel>.FailOutput(Constant.NoObjectFound);
                }

                // Reurn Success Output
                return new Output<StudentModel>
                {
                    Sucess = true,
                    Result = new StudentModel
                    {
                        Id = student.Id,
                        Email = student.Email,
                        Password = student.Password
                    }
                };
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<StudentModel>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }
    }
}
