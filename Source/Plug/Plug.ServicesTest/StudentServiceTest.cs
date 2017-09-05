using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plug.Business;
using Plug.Data;
using Plug.Services;

namespace ServiceTest
{
    [TestClass]
    public class StudentServiceTest
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
        public void VerifyStudent()
        {
            IStudentService studentService = new StudentService(new PlugUnitOfWork(new PlugDbContextFactory()));

            var outputResult = studentService.VerifyStudent(new Plug.Models.Input<Plug.Models.StudentModel>
            {
                OperationBy = "UnitTest",
                Arguments = new Plug.Models.StudentModel
                {
                    Email = "test1@gmail.com",
                    Password = "Donkey@1"
                }
            });

            Assert.IsTrue(outputResult.Sucess);
        }
    }
}
