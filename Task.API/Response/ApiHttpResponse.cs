using System.Linq;
using System.Net;
using System.Collections.Generic;

namespace Task.API.Response
{
    public class ApiHttpResponse
    {
        public bool IsStatusSuccessful { get; set; }
        public dynamic ContentResult { get; set; }
        public List<ErrorStateResponse> ErrorResponseMessageList { get; set; }

        public ApiHttpResponse(HttpStatusCode statusCode, dynamic result = null, List<ErrorStateResponse> errorMessage = null)
        {
            IsStatusSuccessful = false;
            if (statusCode == HttpStatusCode.OK && (errorMessage == null || !errorMessage.Any()))
                IsStatusSuccessful = true;
            if (result != null)
                ContentResult = result;

            if (errorMessage != null && errorMessage.Count > 0)
                ErrorResponseMessageList = errorMessage;
        }
        public ApiHttpResponse()
        {
        }
    }


}
