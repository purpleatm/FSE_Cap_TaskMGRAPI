using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Task.API.Response
{
    public static class BaseResponseMessage
    {
        public static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpStatusCode statusCode, object result = null, List<ErrorStateResponse> errorMessage = null)
        {
            var resultObj = new ApiHttpResponse(statusCode, result, errorMessage);            
            var response = request.CreateResponse(statusCode, resultObj);
            response.Content = response.Content;
            return response;
        }
    }
}
