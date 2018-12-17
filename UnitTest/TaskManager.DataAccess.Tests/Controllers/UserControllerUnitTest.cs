using System;
using NUnit.Framework;
using Task.API.Controllers;
using System.Net;

namespace TaskManager.DataAccess.Tests.Controllers
{
    [TestFixture]
    public class UserControllerUnitTest
    {
        [Test]
        public void GetUsersTestMethod()
        {
            UserController UserController = new UserController();
            var response = UserController.Get();
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void AddUserTestMethod()
        {
            UserController UserController = new UserController();
            var addRequest = new Model.USER_DETAILS()
            {
                First_Name = "test",
                Last_Name = "test",
                Task_ID = 1,
                Project_ID = 1,
                Employee_ID = 12
            };
            var response = UserController.Post(addRequest);
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void UpdateUserTestMethod()
        {
            UserController UserController = new UserController();
            var addRequest = new Model.USER_DETAILS()
            {
                User_ID=1,
                First_Name = "test",
                Last_Name = "test",
                Task_ID = 1,
                Project_ID = 1,
                Employee_ID = 12
            };
            var response = UserController.Post(addRequest);
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void DeleteUserTestMethod()
        {
            UserController UserController = new UserController();
            var addRequest = new Model.USER_DETAILS()
            {
                User_ID = 1002,
            };
            var response = UserController.Delete(addRequest);
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }
    }
}
