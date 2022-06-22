﻿using Microsoft.AspNetCore.Http;
using my_books.Data;
using System;
using System.Net;
using System.Threading.Tasks;

namespace my_books.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
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
                await HandlerExceptionAsync(httpContext, ex);
            }
        }

        private Task HandlerExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var res = new ErrorVM()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message,
                Path = "path here"
            };

            return httpContext.Response.WriteAsync(res.ToString());
        }
    }
}
