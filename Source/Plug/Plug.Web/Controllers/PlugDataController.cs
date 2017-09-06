using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Plug.Services;
using Plug.Models;

namespace Plug_Web.Controllers
{
    /// <summary>
    /// Contorller of Plug Data (Application Program Interface)
    /// </summary>
    [Route("api/[controller]")]
    public class PlugDataController : Controller
    {
        #region Service Interfaces

        /// <summary>
        /// Course Service
        /// </summary>
        public ICourseService CourseService { get; set; }

        /// <summary>
        /// Student Service
        /// </summary>
        public IStudentService StudentService { get; set; }

        /// <summary>
        /// Enrollment Service
        /// </summary>
        public IEnrollmentService EnrollmentService { get; set; }

        #endregion

        #region Constructor

        public PlugDataController(ICourseService courseService, IStudentService studentService, IEnrollmentService enrollmentService)
        {
            CourseService = courseService;
            StudentService = studentService;
            EnrollmentService = enrollmentService;
        }

        #endregion

        #region Get

        /// <summary>
        /// Get Courses
        /// </summary>
        /// <param name="studetnid">Id of the student</param>
        /// <returns>Json Result</returns>
        [HttpGet("[action]")]
        public JsonResult Courses(string studetnid)
        {
            try
            {
                return Json(string.IsNullOrEmpty(studetnid) ? CourseService.GetCourses() : CourseService.GetCourses(new Input<Guid> { Arguments = new Guid(studetnid) }));
            }
            catch (Exception exception)
            {
                return Json(Output<List<CourseModel>>.FailOutput(exception.Message));
            }
        }

        /// <summary>
        /// Get Enrolled Courses
        /// </summary>
        /// <param name="enrolledid">Id of enrollement</param>
        /// <returns>Json Result</returns>
        [HttpGet("[action]")]
        public JsonResult EnrolledCourse(string enrolledid)
        {
            try
            {
                return Json(EnrollmentService.GetEnrollment(new Input<Guid> { OperationBy = "System", Arguments = new Guid(enrolledid) }));
            }
            catch (Exception exception)
            {
                return Json(Output<EnrollmentModel>.FailOutput(exception.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courceId">Id of the course</param>
        /// <param name="moduleid">Id of the module</param>
        /// <returns>Json Result</returns>
        [HttpGet("[action]")]
        public JsonResult Module(string courceId, string moduleid)
        {
            try
            {
                return Json(CourseService.GetModule(new Input<KeyValuePair<Guid, Guid>>
                {
                    OperationBy = "System",
                    Arguments = new KeyValuePair<Guid, Guid>(new Guid(courceId), new Guid(moduleid))
                }));
            }
            catch (Exception exception)
            {
                return Json(Output<ModuleModel>.FailOutput(exception.Message));
            }
        }

        #endregion

        #region Post

        /// <summary>
        /// Verify Student Access
        /// </summary>
        /// <param name="studentModel">Object of Student Model</param>
        /// <returns>Json Result</returns>
        [HttpPost("[action]")]
        public JsonResult VerifyStudent([FromBody] StudentModel studentModel)
        {
            try
            {
                return Json(StudentService.VerifyStudent(new Input<StudentModel> { Arguments = studentModel }));
            }
            catch (Exception exception)
            {
                return Json(OutputResult.FailOutputResult(exception.Message));
            }
        }

        /// <summary>
        /// Enroll Course
        /// </summary>
        /// <param name="enrollmentModel">Object of Enrollment Model</param>
        /// <returns>Json Result</returns>
        [HttpPost("[action]")]
        public JsonResult EnrollCourse([FromBody] EnrollmentModel enrollmentModel)
        {
            try
            {
                return Json(EnrollmentService.AddEnrollment(new Input<EnrollmentModel>
                {
                    OperationBy = "System",
                    Arguments = new EnrollmentModel
                    {
                        CourseId = enrollmentModel.CourseId,
                        StudentId = enrollmentModel.StudentId
                    }
                }));
            }
            catch (Exception exception)
            {
                return Json(OutputResult.FailOutputResult(exception.Message));
            }
        }

        /// <summary>
        /// Update Enrolled Module
        /// </summary>
        /// <param name="enrollModuleModel">Object of Enrollment Module Model</param>
        /// <returns>Json Result</returns>
        [HttpPost("[action]")]
        public JsonResult UpdateEnrollModule([FromBody] EnrollModuleModel enrollModuleModel)
        {
            try
            {
                return Json(EnrollmentService.UpdateEnrolledModule(new Input<EnrollModuleModel>
                {
                    OperationBy = "System",
                    Arguments = enrollModuleModel
                }));
            }
            catch (Exception exception)
            {
                return Json(OutputResult.FailOutputResult(exception.Message));
            }
        }

        /// <summary>
        /// Complete Enrolled Module
        /// </summary>
        /// <param name="enrollModuleModel">Object of Enrollment Module Model</param>
        /// <returns>Json Result</returns>
        [HttpPost("[action]")]
        public JsonResult CompleteEnrollModule([FromBody] EnrollModuleModel enrollModuleModel)
        {
            try
            {
                return Json(EnrollmentService.CompleteEnrolledModule(new Input<EnrollModuleModel>
                {
                    OperationBy = "System",
                    Arguments = enrollModuleModel
                }));
            }
            catch (Exception exception)
            {
                return Json(OutputResult.FailOutputResult(exception.Message));
            }
        }

        /// <summary>
        /// Add a course
        /// </summary>
        /// <param name="courseModel">Object of Add Course Model </param>
        /// <returns>Json Result</returns>
        [HttpPost("[action]")]
        public JsonResult AddCourse([FromBody] AddCourseModel courseModel)
        {
            try
            {
                // Convert Model to Data Transafer Object Model which accept in Add Course Method
                var moduleModelDto = new List<ModuleModel>();
                courseModel.Modules
                    .OrderBy(m => m.Order)
                    .ToList()
                    .ForEach(m =>
                    {
                        if (m.Icon == "TEXT")
                        {
                            moduleModelDto.Add(new TextModuleModel
                            {
                                Title = m.Title,
                                Description = m.Description,
                                CanSkip = m.CanSkip
                            });
                        }

                        if (m.Icon == "VIDEO")
                        {
                            moduleModelDto.Add(new VideoModuleModel
                            {
                                Title = m.Title,
                                CanSkip = m.CanSkip,
                                Uri = m.Uri,
                                Duration = new System.TimeSpan(0, 0, 0)
                            });
                        }

                        if (m.Icon == "QUESTION")
                        {
                            moduleModelDto.Add(new QuestionModuleModel
                            {
                                Title = m.Title,
                                CanSkip = m.CanSkip,
                                Text = m.Text,
                                Choices = m.Choices
                            });
                        }
                    });

                var courseModelDto = new CourseModel
                {
                    Subject = courseModel.Subject,
                    Description = courseModel.Description,
                    Modules = moduleModelDto
                };

                return Json(CourseService.AddCourse(new Input<CourseModel>
                {
                    OperationBy = "System",
                    Arguments = courseModelDto
                }));
            }
            catch (Exception exception)
            {
                return Json(OutputResult.FailOutputResult(exception.Message));
            }
        }

        #endregion

    }
}
