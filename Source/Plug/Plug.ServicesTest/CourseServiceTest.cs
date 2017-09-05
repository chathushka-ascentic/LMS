using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Business;
using Plug.Data;
using Plug.Services;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class CourseServiceTest
    {
        [ClassInitialize()]
        public static void ClassSetup(TestContext context)
        {
        }

        [TestInitialize]
        public void TestSetup()
        {
        }

        [TestMethod]
        public void AddCourse()
        {
            ICourseService courseService = new CourseService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var outputResult = courseService.AddCourse(new Plug.Models.Input<Plug.Models.CourseModel>
            {
                OperationBy = "UnitTest",
                Arguments = new Plug.Models.CourseModel
                {
                    Subject = "CourseTest",
                    Description = "Test",
                    Modules = new System.Collections.Generic.List<Plug.Models.ModuleModel>
                    {
                        new Plug.Models.TextModuleModel
                        {
                            Title = "First",
                            Description = "First",
                            CanSkip = true
                        },
                        new Plug.Models.VideoModuleModel
                        {
                            Title = "Second",
                            CanSkip = false,
                            Uri = "Uri",
                            Duration = new System.TimeSpan(1,0,0)
                        },
                        new Plug.Models.QuestionModuleModel
                        {
                            Title = "Third",
                            CanSkip = false,
                            Text = "Third ?",
                            Choices = new System.Collections.Generic.List<Plug.Models.ChoiceModel>
                            {
                                new Plug.Models.ChoiceModel { Option = "op1", IsAnswer = false },
                                new Plug.Models.ChoiceModel { Option = "op2", IsAnswer = false },
                                new Plug.Models.ChoiceModel { Option = "op3", IsAnswer = false },
                                new Plug.Models.ChoiceModel { Option = "op4", IsAnswer = true },
                            }
                        }
                    }
                }
            });

            Assert.IsTrue(outputResult.Result.Sucess);
        }

        [TestMethod]
        public void UpdateCourse()
        {
            ICourseService courseService = new CourseService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var CourseModels = courseService.GetCourses(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });
            var CourseModel = CourseModels.Result.FirstOrDefault();

            CourseModel.Subject = "New";

            var inputAdd = new Plug.Models.Input<Plug.Models.CourseModel>
            {
                OperationBy = "UnitTest",
                Arguments = CourseModel
            };

            var outputResult = courseService.UpdateCourse(inputAdd);

            Assert.IsTrue(CourseModels.Sucess);
            Assert.IsTrue(outputResult.Result.Sucess);
        }

        [TestMethod]
        public void GetCourses()
        {
            ICourseService courseService = new CourseService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var CourseModels = courseService.GetCourses(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });

            Assert.IsTrue(CourseModels.Sucess);
            Assert.IsTrue(CourseModels.Result.Any());
        }
        
        [TestMethod]
        public void GetCourse()
        {
            ICourseService courseService = new CourseService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var CourseModels = courseService.GetCourses(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });
            var CourseModel = CourseModels.Result.FirstOrDefault();
            var targetCourseModel = courseService.GetCourse(new Plug.Models.Input<System.Guid>
            {
                Arguments = CourseModel.Id
            });

            Assert.IsTrue(CourseModels.Sucess);
            Assert.IsTrue(targetCourseModel.Sucess);
        }
        
        [TestMethod]
        public void GetModule()
        {
            ICourseService courseService = new CourseService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var CourseModels = courseService.GetCourses(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });
            var CourseModel = CourseModels.Result.FirstOrDefault();
            var targetModuleModel = courseService.GetModule(new Plug.Models.Input<System.Collections.Generic.KeyValuePair<System.Guid, System.Guid>>
            {
                Arguments = new System.Collections.Generic.KeyValuePair<System.Guid, System.Guid>(CourseModel.Id, CourseModel.Modules.First().Id)
            });

            Assert.IsTrue(CourseModels.Sucess);
            Assert.IsTrue(targetModuleModel.Sucess);
        }
    }
}
