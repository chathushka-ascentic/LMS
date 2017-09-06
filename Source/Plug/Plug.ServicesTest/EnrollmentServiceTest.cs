using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Business;
using Plug.Data;
using Plug.Services;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class EnrollmentServiceTest
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
        public void AddEnrollment()
        {
            IEnrollmentService enrollmentService = new EnrollmentService(new PlugUnitOfWork(new PlugDbContextFactory()));
            ICourseService courseService = new CourseService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var courseModels = courseService.GetCourses(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });
            var courseModel = courseModels.Result.FirstOrDefault();

            var outputResult = enrollmentService.AddEnrollment(new Plug.Models.Input<Plug.Models.EnrollmentModel>
            {
                OperationBy = "UnitTest",
                Arguments = new Plug.Models.EnrollmentModel
                {
                    StudentId = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5"),
                    CourseId = courseModel.Id,
                }
            });

            Assert.IsTrue(outputResult.Result.Sucess);
        }

        [TestMethod]
        public void GetEnrollments()
        {
            IEnrollmentService enrollmentService = new EnrollmentService(new PlugUnitOfWork(new PlugDbContextFactory()));
            
            var outputResult = enrollmentService.GetEnrollments(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });

            Assert.IsTrue(outputResult.Sucess);
        }
        
        [TestMethod]
        public void GetEnrollment()
        {
            IEnrollmentService enrollmentService = new EnrollmentService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var enrollments = enrollmentService.GetEnrollments(new Plug.Models.Input<System.Guid>
            {
                Arguments = new System.Guid("66EA3A48-49E7-41D4-B36A-BBD86922FAC5")
            });
            var enrollment = enrollments.Result.FirstOrDefault();

            var outputResult = enrollmentService.GetEnrollment(new Plug.Models.Input<System.Guid>
            {
                Arguments = enrollment.Id
            });

            Assert.IsTrue(outputResult.Sucess);
        }
    }
}
