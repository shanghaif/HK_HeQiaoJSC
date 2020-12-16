using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;

namespace HaikanSmartTownCockpit.Api.Culture
{
    public class ContextCultureMiddleware
    {
        private readonly RequestDelegate _next;
        public ContextCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext content)
        {
            await _next.Invoke(content);
            AuthContextService.Configure(content);
        }
    }
}
