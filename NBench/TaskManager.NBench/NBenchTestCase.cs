using NBench;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;
using Task.API.Controllers;
using TaskManager.Model;
using System.Collections.Generic;

namespace TaskManager.NBench
{
    public class NBenchTestCase : PerformanceTestStuite<NBenchTestCase>
    {
        #region Variables
        TaskController objTaskManagerController = null;
        TASK_DETAILS objGET_TASK_DETAILS_Result = null;

        private const int AcceptableMinAddThroughput = 500;
        #endregion

        [PerfSetup]
        public void SetUp(BenchmarkContext context)
        {
            objTaskManagerController = new TaskController();
            objGET_TASK_DETAILS_Result = new TASK_DETAILS();
        }

        #region GetParentTask
        /// <summary>
        /// To get Parent task details
        /// </summary>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetParentTask()
        {
            var response = objTaskManagerController.Get();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<PARENT_TASK>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }
        #endregion

        #region GetTaskDetails
        /// <summary>
        /// Method for getting the task details from BL and send back to HTML
        /// </summary>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void GetTaskDetails()
        {
            var response = objTaskManagerController.GetTasks();
            if (response != null)
            {
                var content = response.Content;
                var vlsit = JsonHelper.fromJson<List<TASK_DETAILS>>(content.ReadAsStringAsync().Result);
                var vCount = vlsit?.Count();
            }
        }
        #endregion

        #region InsertTaskDetails
        /// <summary>
        /// Insert the Task details which user entered
        /// </summary>
        /// <param name="objGET_TASK_DETAILS_Result"></param>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void InsertTaskDetails()
        {
            objGET_TASK_DETAILS_Result = new TASK_DETAILS();

            #region Insert new records

            var addRequest = new Model.TASK_DETAILS()
            {
                Task = "SecondTask",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddMonths(2),
                Priority = 3
            };

            #endregion

            #region Update records

            //objGET_TASK_DETAILS_Result.Task_ID = 4;
            //objGET_TASK_DETAILS_Result.Parent_ID = 1;
            //objGET_TASK_DETAILS_Result.Task = "Update new task";
            //objGET_TASK_DETAILS_Result.Start_Date = DateTime.Now;
            //objGET_TASK_DETAILS_Result.End_Date = null;
            //objGET_TASK_DETAILS_Result.Priority = 16;

            #endregion

            var vlsit = objTaskManagerController.Post(addRequest);

        }
        #endregion

        #region UpdateEndTask
        /// <summary>
        /// Update the End task 
        /// </summary>
        /// <param name="objGET_TASK_DETAILS_Result"></param>
        /// <returns></returns>
        [PerfBenchmark(RunMode = RunMode.Iterations, NumberOfIterations = 500, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        public void UpdateEndTask()
        {
            objGET_TASK_DETAILS_Result = new TASK_DETAILS();

            #region Update records

            objGET_TASK_DETAILS_Result.Task_ID = "38FE07D7-4A67-4AC5-AAB1-73F7D528ABAC";
            objGET_TASK_DETAILS_Result.End_Date = DateTime.Now;

            #endregion

            var vlsit = objTaskManagerController.Put(objGET_TASK_DETAILS_Result);
        }
        #endregion 

        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
             objTaskManagerController = null;
             objGET_TASK_DETAILS_Result = null;

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
