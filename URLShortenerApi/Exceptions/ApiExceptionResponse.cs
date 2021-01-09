using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortner.Exceptions
{
    public class ApiExceptionResponse
    {
        public bool isError { get; set; }
        public string Message { get; set; }
        public ApiExceptionResponse(string Message)
        {
            this.Message = Message;
            isError = true;
        }
    }
}
