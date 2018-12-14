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
    public class UserController : ApiController
    {
        private UserApi UserApi { get; set; }
         
        public UserController()
        {
            UserApi = new UserApi();
        }

        // GET api/<controller>/<route>
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public HttpResponseMessage Get()
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                var parentTask = UserApi.GetUsers();
                return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, parentTask, errors);
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
        /// Add user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(USER_DETAILS request)
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            {
                if (request != null)
                {
                    if (request.User_ID != null && request.Task_ID > 0)
                    {
                            bool transactionStatus = UserApi.UpdateUsers(request);
                            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
                    }
                    else
                    {
                            bool transactionStatus = UserApi.AddUsers(request);
                            return BaseResponseMessage.BuildApiResponse(Request, HttpStatusCode.OK, transactionStatus, errors);
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
        
        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(USER_DETAILS request)
        {
            List<ErrorStateResponse> errors = null;
            this.Request = new HttpRequestMessage();
            this.Request.SetConfiguration(new HttpConfiguration());
            try
            { 
                if (request != null)
                {
                    if (request.Task_ID<=0)
                        throw new Exception("Invalid Request");

                    bool transactionStatus = UserApi.DeleteUser(request);
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
