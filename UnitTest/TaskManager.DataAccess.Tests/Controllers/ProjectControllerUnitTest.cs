using System;
using NUnit.Framework;
using Task.API.Controllers;
using System.Net;

namespace TaskManager.DataAccess.Tests.Controllers
{
    [TestFixture]
    public class ProjectControllerUnitTest
    {
        [Test]
        public void GetProjectsUnitTest()
        {
            ProjectController ProjectController = new ProjectController();
            var response = ProjectController.Get();
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void AddProjectUnitTest()
        {
            ProjectController ProjectController = new ProjectController();
            var addRequest = new Model.PROJECT_DETAILS()
            {
                Project="Project",
                Start_Date= DateTime.Now.AddDays(6),
                End_Date= DateTime.Now.AddDays(10),
                Priority=30 
            }; 
            var response = ProjectController.Post(addRequest);
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void GetProjectManagersUnitTest()
        {
            ProjectController ProjectController = new ProjectController();
            var response = ProjectController.GetProjcetManagers();
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }
        
        [Test]
        public void GetProjectNameUnitTest()
        {
            ProjectController ProjectController = new ProjectController();
            var response = ProjectController.GetProjcetNames();
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }

        [Test]
        public void EndProjectUnitTest()
        {
            ProjectController ProjectController = new ProjectController();
            var request = new Model.PROJECT_DETAILS()
            {
                Project_ID= 1002
            };
            var response = ProjectController.Put(request);
            Assert.NotNull(response, "API failed-Internal Error");
            Assert.AreEqual(HttpStatusCode.OK.ToString().ToLower(), response.StatusCode.ToString().ToLower(), "API failed-Status error");
        }
    }
}
