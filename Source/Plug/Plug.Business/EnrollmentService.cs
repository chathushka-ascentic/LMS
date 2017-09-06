using Plug.DataRepositories;
using Plug.Models;
using Plug.Resources;
using Plug.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plug.Business
{
    public class EnrollmentService : ServiceBase, IEnrollmentService
    {
        /// <summary>
        /// Constructor of the class Enrollment Service
        /// </summary>
        /// <param name="unitOfWork">Unit of Work</param>
        public EnrollmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Add an enrollment
        /// </summary>
        /// <param name="input">Object of Enrollment Model</param>
        /// <returns>Result of Adding an enrollment</returns>
        public async Task<OutputResult> AddEnrollment(Input<EnrollmentModel> input)
        {
            try
            {
                // Input validate
                if (!input.Validate)
                {
                    return OutputResult.FailOutputResult(Constant.InvalidInput);
                }

                // Variable init
                var datetime = DateTime.UtcNow;
                var model = input.Arguments;

                // Retrive Entities
                var enrollment = UnitOfWork.Enrollments.FirstOrDefault(e => e.StudentId == model.StudentId & e.CourseId == model.CourseId);

                // Validate ENtities
                if (enrollment != null)
                {
                    return OutputResult.FailOutputResult(Constant.AlreadyEnrolled);
                }

                // Process Entities
                var enrollmentId = Generator.UniqueIdentifier;
                var course = UnitOfWork.Courses.GetCourse(model.CourseId);
                var enrollModules = course.Modules.Select(m => new Entities.EnrollModule
                {
                    ModuleId = m.Id,
                    EnrollmentId = enrollmentId,
                    IsCompleted = false,
                    IsStarted = false
                }).ToList();

                // Adding Enrollment
                UnitOfWork.Enrollments.Add(new Entities.Enrollment
                {
                    Id = enrollmentId,
                    CreateBy = input.OperationBy,
                    UpdateBy = input.OperationBy,
                    CreateDate = datetime,
                    UpdateDate = datetime,
                    CourseId = model.CourseId,
                    StudentId = model.StudentId,
                    EnrollModules = enrollModules
                });

                // Commit Transaction
                await UnitOfWork.Commit();

                // Success Output
                return OutputResult.SuccessOutputResult();
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return OutputResult.FailOutputResult(Constant.ErrorOccured, description: exception.ToString());
            }
        }

        /// <summary>
        /// Get an enrollment by enrollment id
        /// </summary>
        /// <param name="input">Enrollment id</param>
        /// <returns>Object of Enrollment Model</returns>
        public Output<EnrollmentModel> GetEnrollment(Input<Guid> input)
        {
            try
            {
                // Input validate
                if (!input.Validate)
                {
                    return Output<EnrollmentModel>.FailOutput(Constant.InvalidInput);
                }
                
                // Retrive Entities
                var enrollment = UnitOfWork.Enrollments.FirstOrDefault(e => e.Id == input.Arguments, includeProperties: "Course,EnrollModules,EnrollModules.Module");

                // Validate Entities
                if (enrollment == null)
                {
                    return Output<EnrollmentModel>.FailOutput(Constant.NoObjectFound);
                }

                // Process ENtities
                var enrollModuleModel = new List<EnrollModuleModel>();

                enrollment.EnrollModules.ToList().ForEach(e =>
                {
                    enrollModuleModel.Add(new EnrollModuleModel
                    {
                        EnrollmentId = e.EnrollmentId,
                        IsCompleted = e.IsCompleted,
                        IsStarted = e.IsStarted,
                        LastVisited = e.LastVisited,
                        ModuleId = e.ModuleId,
                        Module = new ModuleModel
                        {
                            Id = e.ModuleId,
                            Title = e.Module.Title,
                            CanSkip = e.Module.CanSkip,
                            Icon = e.Module.Icon,
                            Order = e.Module.Order,
                        }
                    });
                });

                var enrollmentModel = new EnrollmentModel
                {
                    CourseModel = new CourseModel
                    {
                        Subject = enrollment.Course.Subject,
                        Description = enrollment.Course.Description
                    },
                    CourseId = enrollment.CourseId,
                    StudentId = enrollment.StudentId,
                    Id = enrollment.Id,
                    EnrollModulesCount = enrollment.EnrollModules.Count,
                    CompletedEnrollModulesCount = enrollment.EnrollModules.Count(em => em.IsCompleted),
                    EnrollModules = enrollModuleModel
                };

                // Return Output
                return new Output<EnrollmentModel>
                {
                    Sucess = true,
                    Result = enrollmentModel
                };
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<EnrollmentModel>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }

        /// <summary>
        /// Get all enrollments by student id
        /// </summary>
        /// <param name="input">Student id</param>
        /// <returns>List of Enrollments</returns>
        public Output<IList<EnrollmentModel>> GetEnrollments(Input<Guid> input)
        {
            try
            {
                // Validate Input
                if (!input.Validate)
                {
                    return Output<IList<EnrollmentModel>>.FailOutput(Constant.InvalidInput);
                }

                // Variable Init
                var enrollmentModels = new List<EnrollmentModel>();
                
                // Retrive Entities
                var enrollments = UnitOfWork.Enrollments.Get(e => e.StudentId == input.Arguments, includeProperties: "Course,EnrollModules").ToList();

                // Validate Entities
                if (enrollments == null)
                {
                    return Output<IList<EnrollmentModel>>.FailOutput(Constant.NoObjectFound);
                }

                // Process Entities
                enrollments.ForEach(e =>
                {
                    enrollmentModels.Add(new EnrollmentModel
                    {
                        CourseModel = new CourseModel
                        {
                            Subject = e.Course.Subject,
                            Description = e.Course.Description
                        },
                        CourseId = e.CourseId,
                        StudentId = e.StudentId,
                        Id = e.Id,
                        EnrollModulesCount = e.EnrollModules.Count,
                        CompletedEnrollModulesCount = e.EnrollModules.Count(em => em.IsCompleted)
                    });
                });

                // Return Output 
                return new Output<IList<EnrollmentModel>>
                {
                    Sucess = true,
                    Result = enrollmentModels
                };
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<IList<EnrollmentModel>>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }

        /// <summary>
        /// Update Enrolled Module
        /// </summary>
        /// <param name="input">Object of Enrollem Module Model</param>
        /// <returns>Result of Update an enrolled module</returns>
        public async Task<OutputResult> UpdateEnrolledModule(Input<EnrollModuleModel> input)
        {
            try
            {
                // Validate Input
                if (!input.Validate)
                {
                    return OutputResult.FailOutputResult(Constant.InvalidInput);
                }

                // Variable Init
                var model = input.Arguments;
                
                // Retrive Entities
                var completedenrollment = UnitOfWork.Enrollments.GetEnrollModule(model.CompletedModuleId, model.CompletedEnrollmentId);

                // Validate Entities
                if (completedenrollment != null)
                {
                    completedenrollment.IsCompleted = true;
                }

                // Retrive Entities
                var enrollment = UnitOfWork.Enrollments.GetEnrollModule(model.ModuleId, model.EnrollmentId);

                // Validate Entities
                if (enrollment == null)
                {
                    return OutputResult.FailOutputResult(Constant.AlreadyEnrolled);
                }

                // Process Entities
                enrollment.IsStarted = true;
                enrollment.LastVisited = DateTime.Now;

                // Commit Transaction
                await UnitOfWork.Commit();

                // Retrun Success
                return OutputResult.SuccessOutputResult();
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return OutputResult.FailOutputResult(Constant.ErrorOccured, description: exception.ToString());
            }
        }
        
        /// <summary>
        /// Complete Enrolled Module
        /// </summary>
        /// <param name="input">Object of Enrollem Module Model</param>
        /// <returns>Result of Update an enrolled module</returns>
        public async Task<OutputResult> CompleteEnrolledModule(Input<EnrollModuleModel> input)
        {
            try
            {
                // Input Validation 
                if (!input.Validate)
                {
                    return OutputResult.FailOutputResult(Constant.InvalidInput);
                }

               // Varciable Init
                var model = input.Arguments;

                // Retrive Entities
                var enrollment = UnitOfWork.Enrollments.GetEnrollModule(model.ModuleId, model.EnrollmentId);
                
                // Process Entities
                enrollment.IsCompleted = true;
                enrollment.LastVisited = DateTime.Now;

                // Commit Transaction
                await UnitOfWork.Commit();

                // Retrun Success Output
                return OutputResult.SuccessOutputResult();
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return OutputResult.FailOutputResult(Constant.ErrorOccured, description: exception.ToString());
            }
        }
    }
}
