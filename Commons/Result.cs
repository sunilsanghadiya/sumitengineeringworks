using System;
using System.Net;

namespace sew.Commons
{
    public class Result 
    {
        public StatusType Status {get; set;}
        public HttpStatusCode HttpStatusCode { get; set; }
        public string? Message { get; set; }
        public string? MessageCode { get; set; }
        public Object? ResultObject { get; set; }
        public Exception? ResultException { get; set; }
        public bool HasError => Status == StatusType.Error;
        public object WebApiError => new { Message, MessageCode };
        public object ApiResult
        {
            get
            {
                if(HttpStatusCode == HttpStatusCode.OK)
                {
                    return new
                    {
                        StatusCode = (int)HttpStatusCode,
                        Data = ResultObject
                    };
                }
                return new 
                {
                    StatusCode = (int)HttpStatusCode,
                    Message = Message,
                    MessageCode = MessageCode,
                    Data = ResultObject
                };
            }
        }

        public string? ErrorMessage => Message + 
                    ((ResultException != null && !string.IsNullOrEmpty(ResultException!.StackTrace)) ? 
                    ("; Error " + ResultException!.Message + "; StackTrace: " + ResultException!.StackTrace) 
                    : "");
                    
        public int statusCode => (int)HttpStatusCode;
        public Result()
        {
            Status = StatusType.Success;
            HttpStatusCode = HttpStatusCode.OK;
        }

        public Result(string message, Exception? exp)
        {
            Status = StatusType.Error;
            HttpStatusCode = HttpStatusCode.BadRequest;
            MessageCode = message;
            if (exp != null)
            {
                ResultException = exp;
            }
        }

        public Result(string message, string? messageCode = null, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, Exception? exp = null)
        {
            Status = StatusType.Error;
            Message = message;
            MessageCode = messageCode;
            HttpStatusCode = httpStatusCode;
            if (exp != null)
            {
                ResultException = exp;
            }
        }

    }

   

    public enum StatusType
    {
        Success,
        Error
    }
}
