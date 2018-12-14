using System;
using System.Net;
using NUnit.Framework;
using Task.API.Controllers;
using Task.API;

namespace TaskManager.DataAccess.Tests
{
    [TestFixture]
    public class TaskControllerUnitTest
    {

        [Test]
        public void AppStartTestMethod()
        {
            var webApiApplication = new WebApiApplication();
        }

        [Test]
        public void WebApiConfigRegisterTestMethod()
        {
            var conifg = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(conifg);
        }

        [Test]
        public void GetParentsTestMethod()
        {
            TaskController TaskController = new TaskController();
            Assert.NotNull(TaskController.Get(), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.Get().StatusCode.ToString().ToLower(), "API failed-Status error");
        }
        
        [Test]
        public void GetTasksTestMethod()
        {
            TaskController TaskController = new TaskController();
            Assert.NotNull(TaskController.GetTasks(), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.GetTasks().StatusCode.ToString().ToLower(), "API failed-Status error");
        }
        
        [Test]
        public void PostAddTaskWOPTestMethod()
        {
            TaskController TaskController = new TaskController();
            var addRequest = new Model.TASK_DETAILS()
            {
                Task ="ThridTask",
                Start_Date=DateTime.Now,
                End_Date=DateTime.Now.AddMonths(2),
                Priority=3
            };
            Assert.NotNull(TaskController.Post(addRequest), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.Post(addRequest).StatusCode.ToString().ToLower(), "API failed-Status error");
        }
        
        [Test]
        public void PostAddTaskWPTestMethod()
        {
            TaskController TaskController = new TaskController();
            var addRequest = new Model.TASK_DETAILS()
            {
                Parent_ID = 2,
                Task = "second task sub",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 2
            };
            Assert.NotNull(TaskController.Post(addRequest), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.Post(addRequest).StatusCode.ToString().ToLower(), "API failed-Status error");
        }
        
        [Test]
        public void PostUpdateTaskTestMethod()
        {
            TaskController TaskController = new TaskController();
            var addRequest = new Model.TASK_DETAILS()
            {
                Task_ID= 1,
                Task = "first first task sub",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 3
            };
            Assert.NotNull(TaskController.Post(addRequest), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.Post(addRequest).StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void PutEndTaskTestMethod()
        {
            TaskController TaskController = new TaskController();
            var addRequest = new Model.TASK_DETAILS()
            {
                Task_ID = 3,
                End_Date = DateTime.MinValue
            };
            Assert.NotNull(TaskController.Put(addRequest), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.Put(addRequest).StatusCode.ToString().ToLower(), "API failed-Status error");
        }
    }
}
