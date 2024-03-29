﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Sample.Navigation;
using Sample.Views;
using Sample.Data;

namespace Sample
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {

#if DEBUG
            serviceCollection.AddSingleton<IConfiguration, ConfigurationDebug>();
#elif RELEASE
			serviceCollection.AddSingleton<IConfiguration, ConfigurationRelease>();
#endif

            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<IMoviesRepository, MoviesRepository>();

            serviceCollection.AddTransient<LoginViewModel>();
            serviceCollection.AddTransient<HomeViewModel>();

            return serviceCollection;
        }
    }
}
