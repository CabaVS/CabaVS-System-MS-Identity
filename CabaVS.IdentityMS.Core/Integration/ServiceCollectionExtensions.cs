using CabaVS.IdentityMS.Core.Services;
using CabaVS.IdentityMS.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CabaVS.IdentityMS.Core.Integration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreLayer(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddScoped<IUserService, UserService>();
        }
    }
}