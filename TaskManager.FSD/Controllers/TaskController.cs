using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.FSD.Core.ApiHelper.Response;
using TaskManager.Model;

namespace TaskManager.FSD.Controllers
{
    public class TaskController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Get Task details based on criteria
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            List<ErrorStateResponse> errors = null;
            if (request != null)
            {
                TaskModel consumerInfo = request.Content.ReadAsAsync<TaskModel>().Result;
                if (ModelState.IsValid)
                {


                    return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
                }
                else
                {
                    ///Model Validation Failed
                    errors = new List<ErrorStateResponse>();
                    errors = ErrorStateResponse.BuildErrorList(ModelState);
                    return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
                }
            }
            ///Invalid request
            errors = new List<ErrorStateResponse>();
            errors.Add(ErrorStateResponse.BuildErrorMessage("Invalid request"));
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.BadRequest, null, errors);
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get A Task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public HttpResponseMessage GetById(int id)
        {
            List<ErrorStateResponse> errors = null;
            if (id > 0)
            {


            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        }

        // POST api/<controller>
        /// <summary>
        /// Add Task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            List<ErrorStateResponse> errors = null;
            if (request != null)
            {


            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Edit Task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            List<ErrorStateResponse> errors = null;
            if (id > 0)
            {


            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete Task
        /// </summary>
        /// <param name="id"></param>
        public HttpResponseMessage Delete(int id)
        {
            List<ErrorStateResponse> errors = null;
            if (id > 0)
            {


            }
            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, null, errors);
        }
    }
}