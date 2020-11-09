using System;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace CourseProject.MiddleWares
{
    public class DbInitializMiddleWare
    {
        private readonly RequestDelegate _next;

        public DbInitializMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, ApplicationContext context)
        {
            DbInitializer.Initialize(context);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DbInitializeExtensions
    {
        public static IApplicationBuilder UseInitializeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializMiddleWare>();
        }
    }
}
