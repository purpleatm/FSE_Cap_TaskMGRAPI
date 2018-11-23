using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.FSD.Core.ApiHelper.Response;
using TaskManager.Model;
using TaskManager.Business;
using TaskManager.Business.Extenstion;

namespace TaskManager.FSD.Controllers
{
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
        [Route("GetParents")]
        public HttpResponseMessage GetParents()
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
        [Route("GetTasks")]
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
        [Route("End")]
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

        //// GET api/<controller>/5
        ///// <summary>
        ///// Get A Task
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetById")]
        //public HttpResponseMessage GetById(int id)
        //{
        //    List<ErrorStateResponse> errors = null;
        //    if (id > 0)
        //    {
        //        HttpRequestMessage request = null;
        //        TASK_DETAILS consumerInfo = request.Content.ReadAsAsync<TASK_DETAILS>().Result;
        //        if (ModelState.IsValid)
        //        {


        //            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        //        }
        //        else
        //        {
        //            ///Model Validation Failed
        //            errors = new List<ErrorStateResponse>();
        //            errors = ErrorStateResponse.BuildErrorList(ModelState);
        //            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
        //        }
        //    }
        //    return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        //}

       

        //// PUT api/<controller>/5
        ///// <summary>
        ///// Edit Task
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="value"></param>
        //public HttpResponseMessage Put(int id, [FromBody]string value)
        //{
        //    List<ErrorStateResponse> errors = null;
        //    if (id > 0)
        //    {


        //    }
        //    return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        //}

        //// DELETE api/<controller>/5
        ///// <summary>
        ///// Delete Task
        ///// </summary>
        ///// <param name="id"></param>
        //public HttpResponseMessage Delete(int id)
        //{
        //    List<ErrorStateResponse> errors = null;
        //    if (id > 0)
        //    {


        //    }
        //    return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        //}
    }
}