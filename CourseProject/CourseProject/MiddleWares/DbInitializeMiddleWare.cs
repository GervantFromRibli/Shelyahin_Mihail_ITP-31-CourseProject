using System.Threading.Tasks;
using CourseProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CourseProject.MiddleWares
{
    // Компонент middleware для инициализации базы данных
    public class DbInitializMiddleWare
    {
        // Ссылка на следующий компонент
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

    // Метод расширения для добавления компонента middleware
    public static class DbInitializeExtensions
    {
        public static IApplicationBuilder UseInitializeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializMiddleWare>();
        }
    }
}
