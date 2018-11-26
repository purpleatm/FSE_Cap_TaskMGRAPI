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

        public static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpStatusCode statusCode, object result = null, List<ErrorStateResponse> errorMessage = null,List<Header> header = null)
        {
            var resultObj = new ApiHttpResponse(statusCode, result, errorMessage);            
            var response = request.CreateResponse(statusCode, resultObj);
            if (header != null)
            {
                foreach (var head in header)
                {
                    response.Headers.Add(head.HeaderName, head.HeaderValue);
                }
            }
            return response;
        }

        public static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpStatusCode statusCode, object result = null, List<ErrorStateResponse> errorMessage = null, JsonSerializerSettings settings= null)
        {
            string serializedResult = JsonConvert.SerializeObject(result, settings);
            if (result != null)
            {
                if (result.GetType().IsGenericType)
                {
                    result = JArray.Parse(serializedResult);
                }
                else
                {
                    result = JObject.Parse(serializedResult);
                }
            }
            var resultObj = new ApiHttpResponse(statusCode, result, errorMessage);
            var response = request.CreateResponse(statusCode, resultObj);
            return response;
        }
    }
}
