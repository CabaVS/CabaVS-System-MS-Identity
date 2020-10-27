using AutoMapper;
using CabaVS.IdentityMS.API.Configuration;
using CabaVS.IdentityMS.API.Services;
using CabaVS.IdentityMS.API.Services.Abstractions;
using CabaVS.IdentityMS.Core.Integration;
using CabaVS.IdentityMS.Core.Services.Abstractions;
using CabaVS.IdentityMS.Infrastructure.Contexts;
using CabaVS.IdentityMS.Infrastructure.Integration;
using CabaVS.Shared.AspNetCore.API.DTO;
using CabaVS.Shared.AspNetCore.EFCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;

namespace CabaVS.IdentityMS.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Environment = env ?? throw new ArgumentNullException(nameof(env));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors();

            services.Configure<TokenGenerationConfiguration>(Configuration.GetSection("TokenGeneration"));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage);

                    var errorMsg = $"One or multiple validation errors occurred. {string.Join(' ', errors)}";
                    return new BadRequestObjectResult(ErrorDto.FromMessage(errorMsg));
                };
            });

            services.AddAutoMapper(cfg =>
            {
                // add AutoMapper profiles here...
            });

            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddCoreLayer();
            services.AddInfrastructureLayer(Configuration);

            if (Environment.IsDevelopment())
            {
                services.AddSwaggerGen();
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity MS"));
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var result = JsonConvert.SerializeObject(ErrorDto.FromRequestId(context.TraceIdentifier));
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.RunMigrations<IdentityDbContext>(true, Configuration.GetConnectionString("Default"));
        }
    }
}