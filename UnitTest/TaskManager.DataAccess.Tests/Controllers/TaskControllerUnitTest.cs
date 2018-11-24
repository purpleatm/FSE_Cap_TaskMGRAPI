using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManager.FSD.Controllers;

namespace TaskManager.DataAccess.Tests
{
    [TestFixture]
    public class TaskControllerUnitTest
    {
        TaskController TaskController = new TaskController();

        /// <summary>
        /// Test method to get Parent task details
        /// </summary>
        /// <returns></returns>
        [Test]
        public void GetParentsTestMethod()
        {
            var response = TaskController.GetParents();
            if (response != null)
            {
                if (response.StatusCode.ToString().ToLower() == System.Net.HttpStatusCode.OK.ToString().ToLower())
                {

                }
                else
                {
                    Assert.Fail("API failed-Status error");
                }
            }
            else
            {
                Assert.Fail("API failed-Internal Error");
            }
        }


        /// <summary>
        /// Test method to get task details
        /// </summary>
        /// <returns></returns>
        [Test]
        public void GetTasksTestMethod()
        {
            var response = TaskController.GetTasks();
            if (response != null)
            {
                if (response.StatusCode.ToString().ToLower() == System.Net.HttpStatusCode.OK.ToString().ToLower())
                {

                }
                else
                {
                    Assert.Fail("API failed-Status error");
                }
            }
            else
            {
                Assert.Fail("API failed-Internal Error");
            }
        }


        /// <summary>
        /// Test method to check add task
        /// </summary>
        /// <returns></returns>
        [Test]
        public void PostAddTaskWOPTestMethod()
        {
            var addRequest = new Model.TASK_DETAILS()
            {
                Task ="SecondTask",
                Start_Date=DateTime.Now,
                End_Date=DateTime.Now.AddMonths(2),
                Priority=3
            };
            var response = TaskController.Post(addRequest);
            if (response != null)
            {
                if (response.StatusCode.ToString().ToLower() == System.Net.HttpStatusCode.OK.ToString().ToLower())
                {

                }
                else
                {
                    Assert.Fail("API failed-Status error");
                }
            }
            else
            {
                Assert.Fail("API failed-Internal Error");
            }
        }

        /// <summary>
        /// Test method to check add task
        /// </summary>
        /// <returns></returns>
        [Test]
        public void PostAddTaskWPTestMethod()
        {
            var addRequest = new Model.TASK_DETAILS()
            {
                Parent_ID = Guid.Parse("FD202363-BDE4-42D9-B52C-A1AC6A5EF084").ToString(),
                Task = "second task sub",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 2
            };
            var response = TaskController.Post(addRequest);
            if (response != null)
            {
                if (response.StatusCode.ToString().ToLower() == System.Net.HttpStatusCode.OK.ToString().ToLower())
                {

                }
                else
                {
                    Assert.Fail("API failed-Status error");
                }
            }
            else
            {
                Assert.Fail("API failed-Internal Error");
            }
        }

        /// <summary>
        /// Test method to check update task
        /// </summary>
        /// <returns></returns>
        [Test]
        public void PostUpdateTaskTestMethod()
        {
            var addRequest = new Model.TASK_DETAILS()
            {
                //Parent_ID = Guid.Parse("38FE07D7-4A67-4AC5-AAB1-73F7D528ABAC").ToString(),
                Task_ID= "38FE07D7-4A67-4AC5-AAB1-73F7D528ABAC",
                Task = "first task-modifed",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 3
            };
            var response = TaskController.Post(addRequest);
            if (response != null)
            {
                if (response.StatusCode.ToString().ToLower() == System.Net.HttpStatusCode.OK.ToString().ToLower())
                {

                }
                else
                {
                    Assert.Fail("API failed-Status error");
                }
            }
            else
            {
                Assert.Fail("API failed-Internal Error");
            }
        }

        /// <summary>
        /// Test method to check update end task
        /// </summary>
        /// <returns></returns>
        [Test]
        public void PutEndTaskTestMethod()
        {
            var addRequest = new Model.TASK_DETAILS()
            {
                Task_ID = "F3BF627B-9EA7-4C9C-9A17-B876A2EC3CB7",
                End_Date = DateTime.MinValue,
            };
            var response = TaskController.Put(addRequest);
            if (response != null)
            {
                if (response.StatusCode.ToString().ToLower() == System.Net.HttpStatusCode.OK.ToString().ToLower())
                {

                }
                else
                {
                    Assert.Fail("API failed-Status error");
                }
            }
            else
            {
                Assert.Fail("API failed-Internal Error");
            }
        }

    }
}
