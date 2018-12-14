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
    public class ProjectController : ApiController
    {
        private ProjectApi projectApi { get; set; }
         
        public ProjectController()
        {
            projectApi = new ProjectApi();
        }

        // GET api/<controller>/<route>
        /// <summary>
        /// Get projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("projects")]
        public HttpResponseMessage Get()
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                var parentTask = projectApi.GetProjects();
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
        [Route("projectnames")]
        public HttpResponseMessage GetProjcetNames()
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                var tasks = projectApi.GetProjectName();
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
        public HttpResponseMessage Post(PROJECT_DETAILS request)
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                if (request != null)
                {
                    if (request.Project_ID != null && request.Project_ID > 0)
                    {
                        //if (request.IsUpdateTaskModelValid())
                        //{
                            bool transactionStatus = projectApi.UpdateProject(request);
                            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
                        //}
                    }
                    else
                    {
                        //if (request.IsAddTaskModelValid())
                        //{
                            bool transactionStatus = projectApi.AddProject(request);
                            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
                        //}
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
        public HttpResponseMessage Put(PROJECT_DETAILS request)
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            { 
                if (request != null)
                {
                    if (request.Project_ID <= 0)
                        throw new Exception("Invalid Request");

                    bool transactionStatus = projectApi.EndProejct(request);
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
    }
}
