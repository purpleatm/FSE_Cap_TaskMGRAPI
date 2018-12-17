using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using NBench;
using Task.API.Controllers;
using TaskManager.Model;


namespace TaskManager.NBench
{
    public class NBenchTestCase : PerformanceTestStuite<NBenchTestCase>
    {
        #region Variables
        TaskController taskMgrController = null;
        UserController userController = null;
        ProjectController projectController = null;
        private const int AcceptableMinAddThroughput = 500;
        #endregion

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            taskMgrController = new TaskController();
            userController = new UserController();
            projectController = new ProjectController();
        }

        #region Task load test
        /// <summary>
        /// To get Parent task details
        /// </summary>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetParentTask()
        {
            var response = taskMgrController.Get();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<PARENT_TASK>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }

        /// <summary>
        /// Get task details
        /// </summary>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetTaskDetails()
        {
            var response = taskMgrController.GetTasks();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<TASK_DETAILS>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }

        /// <summary>
        /// Insert the Task details 
        /// </summary>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void InsertTaskDetails()
        {
            var addRequest = new Model.TASK_DETAILS()
            {
                Task = "SecondTask",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 3
            };
            var vlsit = taskMgrController.Post(addRequest);
        }

        /// <summary>
        /// Update the End task 
        /// </summary        
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void UpdateEndTask()
        {
            var request = new TASK_DETAILS();
            request.Task_ID = 1;
            request.End_Date = DateTime.Now;
            var vlsit = taskMgrController.Put(request);
        }
        #endregion

        #region User load test
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetUsers()
        {
            var response = userController.Get();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<USER_DETAILS>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }
        
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void InsertUserDetails()
        {
            var addRequest = new Model.USER_DETAILS()
            {
                First_Name = "test",
                Last_Name = "test",
                Task_ID = 1,
                Project_ID = 1,
                Employee_ID = 12
            };
            var vlsit = userController.Post(addRequest);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void UpdateUserDetails()
        {
            UserController UserController = new UserController();
            var updateReqeust = new Model.USER_DETAILS()
            {
                User_ID = 1,
                First_Name = "test",
                Last_Name = "test",
                Task_ID = 1,
                Project_ID = 1,
                Employee_ID = 12
            };
            var vlsit = userController.Post(updateReqeust);
        }
        
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void DeleteUser()
        {
            var request = new Model.USER_DETAILS()
            {
                User_ID = 1002,
            };
            var vlsit = userController.Delete(request);
        }
        #endregion

        #region Project load test
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetProjects()
        {
            var response = projectController.Get();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<PROJECT_DETAILS>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetProjectNames()
        {
            var response = projectController.GetProjcetNames();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<PROJECT_DETAILS>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetProjcetManagers()
        {
            var response = projectController.GetProjcetManagers();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<PROJECT_DETAILS>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void AddprojectDetails()
        {
            UserController UserController = new UserController();
            var addRequest = new Model.PROJECT_DETAILS()
            {
                Project = "Project"+new Random().ToString(),
                Start_Date = DateTime.Now.AddDays(6),
                End_Date = DateTime.Now.AddDays(10),
                Priority = 30
            };
            var vlsit = projectController.Post(addRequest);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void UpdateProjectDetails()
        {
            UserController UserController = new UserController();
            var addRequest = new Model.PROJECT_DETAILS()
            {
                Project_ID=1,
                Project = "Project",
                Start_Date = DateTime.Now.AddDays(6),
                End_Date = DateTime.Now.AddDays(10),
                Priority = 30
            };
            var vlsit = projectController.Post(addRequest);
        }

        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void DeleteProject()
        {
            var request = new Model.PROJECT_DETAILS()
            {
                Project_ID = 1002
            };
            var vlsit = projectController.Put(request);
        }
        #endregion

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
             taskMgrController = null;
        }
    }

    public class JsonHelper
    {
        public static T fromJson<T>(string json)
        {
            var bytes = Encoding.Unicode.GetBytes(json);

            using (MemoryStream mst = new MemoryStream(bytes))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(mst);
            }
        }

        public static string toJson(object instance)
        {
            using (MemoryStream mst = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(instance.GetType());
                serializer.WriteObject(mst, instance);
                mst.Position = 0;

                using (StreamReader r = new StreamReader(mst))
                {
                    return r.ReadToEnd();
                }
            }
        }
    }
}
