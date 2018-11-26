using System.Linq;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace TaskManager.FSD.Core.ApiHelper.Response
{
  
    public class ErrorStateResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public static List<ErrorStateResponse> BuildErrorList(ModelStateDictionary  model)
        {
            var errors = new List<ErrorStateResponse>();
            foreach (var state in model)
            {
                foreach (var error in state.Value.Errors)
                {
                    if (error.Exception == null)
                    {
                        string[] errorState = error.ErrorMessage.Split('|');
                        if (errorState?.Count() > 1)
                            errors.Add(new ErrorStateResponse { ErrorCode = errorState[0].ToString(), ErrorMessage = errorState[1].ToString() });
                        else
                            errors.Add(new ErrorStateResponse { ErrorCode = "400", ErrorMessage = errorState[0].ToString() });
                    }
                }
            }

            return errors;
        }

        public static ErrorStateResponse BuildErrorMessage(string errorMessage)
        {
            ErrorStateResponse error = new ErrorStateResponse();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                if (errorMessage.Contains("|"))
                {
                    string[] errorState = errorMessage.Split('|');
                    error.ErrorCode = errorState[0].ToString();
                    error.ErrorMessage = errorState[1].ToString();
                }
                else
                {
                    error = new ErrorStateResponse { ErrorCode = "500", ErrorMessage = errorMessage };
                }
            }
            
            return error;
        }
        public static List<ErrorStateResponse> BuildErrorList(string errMessage)
        {
            var errors = new List<ErrorStateResponse>();
            errors.Add(new ErrorStateResponse { ErrorCode = "500", ErrorMessage = errMessage });
            return errors;
        }
    }
}
