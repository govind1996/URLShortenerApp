using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using URLShortner.Exceptions;

namespace URLShortner.Filters
{
    public class ExceptionHandling : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //handled error
            if(context.Exception is URLShortner.ApiException)
            {
                var ex = context.Exception as ApiException;
                context.Exception = null;
                var ApiError = new ApiExceptionResponse(ex.Message);
                
                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = ex.StatusCode;
                response.ContentType = "application/json";

                context.Result = new JsonResult(ApiError);
            }
            //unhandled error
            else
            {
                var ex = context.Exception as ApiException;
                context.Exception = null;
                var ApiError = new ApiExceptionResponse("Unhandled Exception");

                HttpResponse response = context.HttpContext.Response;
                response.StatusCode = ex.StatusCode;
                response.ContentType = "application/json";

                context.Result = new JsonResult(ApiError);
                //handle logging here
            }


        }
    }
}
