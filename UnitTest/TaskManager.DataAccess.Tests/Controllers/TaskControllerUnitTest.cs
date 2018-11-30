using System;
using System.Net;
using NUnit.Framework;
using Task.API.Controllers;

namespace TaskManager.DataAccess.Tests
{
    [TestFixture]
    public class TaskControllerUnitTest
    {
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
                Start_Date=DateTime.Now.ToString(),
                End_Date=DateTime.Now.AddMonths(2).ToString(),
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
                Parent_ID = Guid.Parse("0F3D2B64-A0A0-4F16-9AF5-15F0EBC717B7").ToString(),
                Task = "second task sub",
                Start_Date = DateTime.Now.ToString(),
                End_Date = DateTime.Now.AddMonths(2).ToString(),
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
                Task_ID= "E8D8E70B-0A9C-48BA-8E0A-5868301B9604",
                Task = "first first task sub",
                Start_Date = DateTime.Now.ToString(),
                End_Date = DateTime.Now.AddMonths(2).ToString(),
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
                Task_ID = "F3BF627B-9EA7-4C9C-9A17-B876A2EC3CB7",
                End_Date = DateTime.MinValue.ToString()
            };
            Assert.NotNull(TaskController.Put(addRequest), "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), TaskController.Put(addRequest).StatusCode.ToString().ToLower(), "API failed-Status error");
        }

    }
}
