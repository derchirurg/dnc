using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public class ReinRausMiddleware
    {
        private RequestDelegate next;
        private string number;

        private WaitService waitService;

        public ReinRausMiddleware(RequestDelegate next,
            IOptions<ReinRausOptions> options,
            WaitService waitService)
        {
            this.next = next;
            this.number = options.Value?.Number;
            this.waitService = waitService;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"{number} rein<br />");
            await waitService.Wait();
            await next(context);
            await waitService.Wait();
            await context.Response.WriteAsync($"{number} raus<br />");
        }
    }
}