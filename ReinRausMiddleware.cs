using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmptyApp
{
    public class ReinRausMiddleware
    {
        private RequestDelegate next;

        public ReinRausMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("Eins rein<br />");
            await next(context);
            await context.Response.WriteAsync("Eins raus<br />");
        }
    }
}