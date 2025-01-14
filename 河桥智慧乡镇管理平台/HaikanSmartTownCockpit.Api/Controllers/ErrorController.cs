﻿using System;
using System.Net;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace HaikanSmartTownCockpit.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("/error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("{code}")]
        [HttpGet]
        public IActionResult Code(int code)
        {
            // 捕获状态码
            var statusCode = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is HttpException httpEx ?
                httpEx.StatusCode : (HttpStatusCode)Response.StatusCode;
            var ex = (HttpException)HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var parsedCode = (HttpStatusCode)code;
            var error = new ErrorDetails
            {
                StatusCode = code,
                Message = ex?.ToString()
            };
            // 如果是ASP.NET Core Web Api 应用程序，直接返回状态码(不跳转到错误页面，这里假设所有API接口的路径都是以/api/开始的)
            if (HttpContext.Features.Get<IHttpRequestFeature>().RawTarget.StartsWith("/api/", StringComparison.Ordinal))
            {
                 parsedCode = (HttpStatusCode)code;
                // error = new ErrorDetails
                //{
                //    StatusCode = code,
                //    Message = parsedCode.ToString()
                //};

                return new ObjectResult(error);
            }

            // error = new ErrorDetails
            //{
            //    StatusCode = code,
            //    Message = parsedCode.ToString()
            //};

            return new ObjectResult(error);
        }
    }
}