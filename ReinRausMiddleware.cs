using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public class ReinRausMiddleware
    {
        private RequestDelegate next;
        private string number;

        public ReinRausMiddleware(RequestDelegate next, IOptions<ReinRausOptions> options)
        {
            this.next = next;
            this.number = options.Value?.Number;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"{number} rein<br />");
            await next(context);
            await context.Response.WriteAsync($"{number} raus<br />");
        }
    }
}