﻿using System;
using System.Net;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Http;

namespace HaikanSmartTownCockpit.Api.Extensions.CustomException
{
    /// <summary>
    /// 异常中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new ErrorDetails {
                StatusCode= 500,
                Message=$"资源服务器忙,请稍候再试,原因:{exception.Message}"
            };
            if (exception is UnauthorizeException)
            {
                error.StatusCode= (int)HttpStatusCode.Unauthorized;
                error.Message = "未授权的访问(未登录或者登录已超时)";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            return context.Response.WriteAsync(error.ToString());
        }
    }
}
