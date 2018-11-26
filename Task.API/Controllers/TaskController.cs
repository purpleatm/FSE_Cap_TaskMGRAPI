using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Task.API.Response;
using TaskManager.Business;
using TaskManager.Business.Extenstion;
using TaskManager.Model;

namespace Task.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        private TaskApi TaskApi { get; set; }
         
        public TaskController()
        {
            TaskApi = new TaskApi();
        }

        // GET api/<controller>/<route>
        /// <summary>
        /// Get parent tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("parents")]
        public HttpResponseMessage Get()
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                var parentTask = TaskApi.GetParentTask();
                return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, parentTask, errors);
            }
            catch (Exception ex)
            {
                errors = new List<ErrorStateResponse>();
                errors.Add(ErrorStateResponse.BuildErrorMessage(ex.Message));
            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
        }

        // GET api/<controller>/<route>
        /// <summary>
        /// Get tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tasks")]
        public HttpResponseMessage GetTasks()
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                var tasks = TaskApi.GetTaskDetails();
                return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, tasks, errors);
            }
            catch (Exception ex)
            {
                errors = new List<ErrorStateResponse>();
                errors.Add(ErrorStateResponse.BuildErrorMessage(ex.Message));
            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
        }

        // POST api/<controller>
        /// <summary>
        /// Add task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(TASK_DETAILS request)
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                if (request != null)
                {
                    if (request.Task_ID != null && request.Task_ID.ToGuid() != Guid.Empty)
                    {
                        if (request.IsUpdateTaskModelValid())
                        {
                            bool transactionStatus = TaskApi.UpdateTaskDetail(request);
                            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
                        }
                    }
                    else
                    {
                        if (request.IsAddTaskModelValid())
                        {
                            bool transactionStatus = TaskApi.AddTaskDetail(request);
                            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
                        }
                    }
                }
                ///Model Validation Failed
                errors = new List<ErrorStateResponse>();
                errors.Add(ErrorStateResponse.BuildErrorMessage("Invalid Request"));
                return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
            }
            catch (Exception ex)
            {
                errors = new List<ErrorStateResponse>();
                errors.Add(ErrorStateResponse.BuildErrorMessage(ex.Message));
            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update end task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("end")]
        public HttpResponseMessage Put(TASK_DETAILS request)
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            { 
                if (request != null)
                {
                    if (!request.Task_ID.ToGuid().IsValidGUID())
                        throw new Exception("Invalid Request");

                    bool transactionStatus = TaskApi.UpdateEndTask(request);
                    return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
                }
                ///Model Validation Failed
                errors = new List<ErrorStateResponse>();
                errors.Add(ErrorStateResponse.BuildErrorMessage("Invalid Request"));
                return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
            }
            catch (Exception ex)
            {
                errors = new List<ErrorStateResponse>();
                errors.Add(ErrorStateResponse.BuildErrorMessage(ex.Message));
            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
        }

        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
