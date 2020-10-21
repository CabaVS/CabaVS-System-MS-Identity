using AutoMapper;
using CabaVS.IdentityMS.Core.Repositories;
using CabaVS.IdentityMS.Infrastructure.AutoMapper;
using CabaVS.IdentityMS.Infrastructure.Contexts;
using CabaVS.IdentityMS.Infrastructure.Repositories;
using CabaVS.Shared.EFCore.UnitOfWork.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CabaVS.IdentityMS.Infrastructure.Integration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string connectionStringName = "Default",
            QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.NoTracking)
        {
            if (serviceCollection == null) throw new ArgumentNullException(nameof(serviceCollection));

            serviceCollection.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            serviceCollection.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringName));
                options.UseQueryTrackingBehavior(queryTrackingBehavior);
            });

            serviceCollection.AddScoped<DbContext, IdentityDbContext>();

            serviceCollection.AddEFCoreUnitOfWork();

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}