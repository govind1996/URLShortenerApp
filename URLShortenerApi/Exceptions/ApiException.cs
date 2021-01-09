using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortner.Dtos;

namespace URLShortner
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public ApiException(string Message,int ExceptionCode=500):base(Message)
        {
            
            StatusCode = ExceptionCode;
        }
    }
}
