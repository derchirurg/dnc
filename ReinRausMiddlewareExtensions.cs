using System;
using Microsoft.Extensions.DependencyInjection;

namespace EmptyApp
{
    public static class ReinRausMiddlewareExtensions
    {
        public static IServiceCollection AddReinRaus(this IServiceCollection collection, Action<ReinRausOptions> setupAction)
        {
            if (setupAction != null)
            {
                collection.Configure(setupAction);
            }

            return collection;
        }
    }
}