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
    /// <summary>
    /// Concrete implmenetation of ICourseService
    /// </summary>
    public class CourseService : ServiceBase, ICourseService
    {
        /// <summary>
        /// Constructor of the classCourse Service
        /// </summary>
        /// <param name="unitOfWork">Unit of Work</param>
        public CourseService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        /// <summary>
        /// Add a course 
        /// </summary>
        /// <param name="input">Object of Course Model</param>
        /// <returns>Result of adding a course</returns>
        public async Task<OutputResult> AddCourse(Input<CourseModel> input)
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
                var modules = new List<Entities.Module>();

                // Model process
                model.Modules.ForEach(m =>
                {
                    if (m is VideoModuleModel)
                    {
                        var video = m as VideoModuleModel;
                        modules.Add(new Entities.Video
                        {
                            Id = Generator.UniqueIdentifier,
                            CreateBy = input.OperationBy,
                            UpdateBy = input.OperationBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            CanSkip = video.CanSkip,
                            Icon = video.Icon,
                            Title = video.Title,
                            Uri = video.Uri,
                            Duration = video.Duration
                        });
                    }

                    if (m is TextModuleModel)
                    {
                        var text = m as TextModuleModel;
                        modules.Add(new Entities.Text
                        {
                            Id = Generator.UniqueIdentifier,
                            CreateBy = input.OperationBy,
                            UpdateBy = input.OperationBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            CanSkip = text.CanSkip,
                            Icon = text.Icon,
                            Title = text.Title,
                            Description = text.Description
                        });
                    }

                    if (m is QuestionModuleModel)
                    {
                        var question = m as QuestionModuleModel;
                        modules.Add(new Entities.Question
                        {
                            Id = Generator.UniqueIdentifier,
                            CreateBy = input.OperationBy,
                            UpdateBy = input.OperationBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            CanSkip = question.CanSkip,
                            Icon = question.Icon,
                            Title = question.Title,
                            Text = question.Text,
                            Choices = question.Choices.Select(c => new Entities.Choice
                            {
                                IsAnswer = c.IsAnswer,
                                Option = c.Option
                            }).ToList()
                        });
                    };
                });

                // Course Adding
                UnitOfWork.Courses.Add(new Entities.Course
                {
                    Id = Generator.UniqueIdentifier,
                    CreateBy = input.OperationBy,
                    UpdateBy = input.OperationBy,
                    CreateDate = datetime,
                    UpdateDate = datetime,
                    Subject = model.Subject,
                    Description = model.Description,
                    Modules = modules
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
        /// Update a course
        /// </summary>
        /// <param name="input">Object of Course Model</param>
        /// <returns>Result of update a course</returns>
        public async Task<OutputResult> UpdateCourse(Input<CourseModel> input)
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
                var modules = new List<Entities.Module>();

                // Model process
                model.Modules.ForEach(m =>
                {
                    if (m is VideoModuleModel)
                    {
                        var video = m as VideoModuleModel;
                        modules.Add(new Entities.Video
                        {
                            Id = video.Id,
                            CreateBy = input.OperationBy,
                            UpdateBy = input.OperationBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            CanSkip = video.CanSkip,
                            Icon = video.Icon,
                            Title = video.Title,
                            Uri = video.Uri,
                            Duration = video.Duration
                        });
                    }

                    if (m is TextModuleModel)
                    {
                        var text = m as TextModuleModel;
                        modules.Add(new Entities.Text
                        {
                            Id = text.Id,
                            CreateBy = input.OperationBy,
                            UpdateBy = input.OperationBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            CanSkip = text.CanSkip,
                            Icon = text.Icon,
                            Title = text.Title,
                            Description = text.Description
                        });
                    }

                    if (m is QuestionModuleModel)
                    {
                        var question = m as QuestionModuleModel;
                        modules.Add(new Entities.Question
                        {
                            Id = question.Id,
                            CreateBy = input.OperationBy,
                            UpdateBy = input.OperationBy,
                            CreateDate = datetime,
                            UpdateDate = datetime,
                            CanSkip = question.CanSkip,
                            Icon = question.Icon,
                            Title = question.Title,
                            Text = question.Text,
                            Choices = question.Choices.Select(c => new Entities.Choice
                            {
                                IsAnswer = c.IsAnswer,
                                Option = c.Option
                            }).ToList()
                        });
                    };
                });

                // Course Update
                UnitOfWork.Courses.Update(new Entities.Course
                {
                    Id = model.Id,
                    CreateBy = input.OperationBy,
                    UpdateBy = input.OperationBy,
                    CreateDate = datetime,
                    UpdateDate = datetime,
                    Subject = model.Subject,
                    Description = model.Description,
                    Modules = modules
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
        /// Get all courses
        /// </summary>
        /// <param name="input">Student Id to check the enrollment</param>
        /// <returns>List of Course Models</returns>
        public Output<IList<CourseModel>> GetCourses(Input<Guid> input)
        {
            try
            {
                // Input validate
                if (!input.Validate)
                {
                    return Output<IList<CourseModel>>.FailOutput(Constant.InvalidInput);
                }

                // Variable init
                var coursesModelList = new List<CourseModel>();

                // Retrive Entities
                var courses = UnitOfWork.Courses.GetCoursesWithChoices();
                var enrollments = UnitOfWork.Enrollments.Get(e => e.StudentId == input.Arguments);

                // Process Entities
                foreach (var course in courses)
                {
                    var moduleModelList = new List<ModuleModel>();

                    foreach (var module in course.Modules)
                    {
                        if (module is Entities.Video)
                        {
                            var video = module as Entities.Video;
                            moduleModelList.Add(new VideoModuleModel
                            {
                                Id = video.Id,
                                CanSkip = video.CanSkip,
                                Order = video.Order,
                                Title = video.Title,
                                Uri = video.Uri,
                                Duration = video.Duration
                            });
                        }

                        if (module is Entities.Text)
                        {
                            var text = module as Entities.Text;
                            moduleModelList.Add(new TextModuleModel
                            {
                                Id = text.Id,
                                CanSkip = text.CanSkip,
                                Order = text.Order,
                                Title = text.Title,
                                Description = text.Description
                            });
                        }

                        if (module is Entities.Question)
                        {
                            var question = module as Entities.Question;
                            moduleModelList.Add(new QuestionModuleModel
                            {
                                Id = question.Id,
                                CanSkip = question.CanSkip,
                                Title = question.Title,
                                Order = question.Order,
                                Text = question.Text,
                                Choices = question.Choices.Select(c => new ChoiceModel
                                {
                                    IsAnswer = c.IsAnswer,
                                    Option = c.Option
                                }).ToList()
                            });
                        };
                    }

                    coursesModelList.Add(new CourseModel
                    {
                        Id = course.Id,
                        Subject = course.Subject,
                        Description = course.Description,
                        Modules = moduleModelList,
                        Enrolled = enrollments.Any(e => e.CourseId == course.Id),
                        EnrolledId = enrollments.FirstOrDefault(e => e.CourseId == course.Id)?.Id
                    });
                }

                // Return Output
                return new Output<IList<CourseModel>>
                {
                    Sucess = true,
                    Result = coursesModelList
                };
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<IList<CourseModel>>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns>List of Course Models</returns>
        public Output<IList<CourseModel>> GetCourses()
        {
            try
            {
                // Variable init
                var coursesModelList = new List<CourseModel>();

                // Retrive Entities
                var courses = UnitOfWork.Courses.GetCoursesWithChoices();

                // Process Entities
                foreach (var course in courses)
                {
                    var moduleModelList = new List<ModuleModel>();

                    foreach (var module in course.Modules)
                    {
                        if (module is Entities.Video)
                        {
                            var video = module as Entities.Video;
                            moduleModelList.Add(new VideoModuleModel
                            {
                                Id = video.Id,
                                CanSkip = video.CanSkip,
                                Order = video.Order,
                                Title = video.Title,
                                Uri = video.Uri,
                                Duration = video.Duration
                            });
                        }

                        if (module is Entities.Text)
                        {
                            var text = module as Entities.Text;
                            moduleModelList.Add(new TextModuleModel
                            {
                                Id = text.Id,
                                CanSkip = text.CanSkip,
                                Order = text.Order,
                                Title = text.Title,
                                Description = text.Description
                            });
                        }

                        if (module is Entities.Question)
                        {
                            var question = module as Entities.Question;
                            moduleModelList.Add(new QuestionModuleModel
                            {
                                Id = question.Id,
                                CanSkip = question.CanSkip,
                                Title = question.Title,
                                Order = question.Order,
                                Text = question.Text,
                                Choices = question.Choices.Select(c => new ChoiceModel
                                {
                                    IsAnswer = c.IsAnswer,
                                    Option = c.Option
                                }).ToList()
                            });
                        };
                    }

                    coursesModelList.Add(new CourseModel
                    {
                        Id = course.Id,
                        Subject = course.Subject,
                        Description = course.Description,
                        Modules = moduleModelList
                    });
                }

                // Return Output
                return new Output<IList<CourseModel>>
                {
                    Sucess = true,
                    Result = coursesModelList
                };
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<IList<CourseModel>>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }

        /// <summary>
        /// Get Course by Course Id
        /// </summary>
        /// <param name="input">Course Id</param>
        /// <returns>Object of Course</returns>
        public Output<CourseModel> GetCourse(Input<Guid> input)
        {
            try
            {
                // Input validate
                if (!input.Validate)
                {
                    return Output<CourseModel>.FailOutput(Constant.InvalidInput);
                }

                // Variable init
                var moduleModelList = new List<ModuleModel>();

                // Retrive Entities
                var course = UnitOfWork.Courses.GetCourse(input.Arguments);

                // Process Entities
                foreach (var module in course.Modules)
                {
                    if (module is Entities.Video)
                    {
                        var video = module as Entities.Video;
                        moduleModelList.Add(new VideoModuleModel
                        {
                            Id = video.Id,
                            CanSkip = video.CanSkip,
                            Order = video.Order,
                            Title = video.Title,
                            Uri = video.Uri,
                            Duration = video.Duration
                        });
                    }

                    if (module is Entities.Text)
                    {
                        var text = module as Entities.Text;
                        moduleModelList.Add(new TextModuleModel
                        {
                            Id = text.Id,
                            CanSkip = text.CanSkip,
                            Order = text.Order,
                            Title = text.Title,
                            Description = text.Description
                        });
                    }

                    if (module is Entities.Question)
                    {
                        var question = module as Entities.Question;
                        moduleModelList.Add(new QuestionModuleModel
                        {
                            Id = question.Id,
                            Order = question.Order,
                            CanSkip = question.CanSkip,
                            Title = question.Title,
                            Text = question.Text
                        });
                    };
                }

                var CourseModel = new CourseModel
                {
                    Id = course.Id,
                    Subject = course.Subject,
                    Description = course.Description,
                    Modules = moduleModelList
                };

                // Return Output
                return new Output<CourseModel>
                {
                    Sucess = true,
                    Result = CourseModel
                };
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<CourseModel>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }

        /// <summary>
        /// Get Module by Course Id and Module Id
        /// </summary>
        /// <param name="input">Course Id and Module Id as Key Value Pair</param>
        /// <returns>Object of Module Model</returns>
        public Output<ModuleModel> GetModule(Input<KeyValuePair<Guid, Guid>> input)
        {
            try
            {
                // Input validate
                if (!input.Validate)
                {
                    return Output<ModuleModel>.FailOutput(Constant.InvalidInput);
                }

                // Variable init
                var moduleModelList = new List<ModuleModel>();

                // Retrive Entities
                var course = UnitOfWork.Courses.GetCourseWithChoices(input.Arguments.Key);
                var module = course.Modules.FirstOrDefault(m => m.Id == input.Arguments.Value);

                // Process Entities
                if (module is Entities.Video)
                {
                    var video = module as Entities.Video;
                    return new Output<ModuleModel>
                    {
                        Sucess = true,
                        Result = new VideoModuleModel
                        {
                            Id = video.Id,
                            CanSkip = video.CanSkip,
                            Order = video.Order,
                            Title = video.Title,
                            Uri = video.Uri,
                            Duration = video.Duration
                        }
                    };
                }

                if (module is Entities.Text)
                {
                    var text = module as Entities.Text;
                    return new Output<ModuleModel>
                    {
                        Sucess = true,
                        Result = new TextModuleModel
                        {
                            Id = text.Id,
                            CanSkip = text.CanSkip,
                            Order = text.Order,
                            Title = text.Title,
                            Description = text.Description
                        }
                    };
                }

                if (module is Entities.Question)
                {
                    var question = module as Entities.Question;
                    return new Output<ModuleModel>
                    {
                        Sucess = true,
                        Result = new QuestionModuleModel
                        {
                            Id = question.Id,
                            CanSkip = question.CanSkip,
                            Title = question.Title,
                            Order = question.Order,
                            Text = question.Text,
                            Choices = question.Choices.Select(c => new ChoiceModel
                            {
                                IsAnswer = c.IsAnswer,
                                Option = c.Option
                            }).ToList()
                        }
                    };
                }

                // Return Output
                return Output<ModuleModel>.FailOutput(Constant.NoObjectFound);
            }
            catch (Exception exception)
            {
                // Failed Output in case of exception
                return Output<ModuleModel>.FailOutput(Constant.ErrorOccured, description: exception.ToString());
            }
        }
    }
}
