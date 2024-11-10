﻿using Core.Contracts;
using Core.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FullApiServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ICalculator), typeof(Calculator));

            return services;
        }
    }
}