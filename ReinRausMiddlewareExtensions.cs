using System;
using Microsoft.Extensions.DependencyInjection;

namespace EmptyApp
{
    public static class ReinRausMiddlewareExtensions
    {
        public static IServiceCollection AddReinRaus(this IServiceCollection services, Action<ReinRausOptions> setupAction)
        {
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return services;
        }
    }
}