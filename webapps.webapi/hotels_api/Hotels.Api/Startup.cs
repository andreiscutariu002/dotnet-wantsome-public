namespace Hotels.Api
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Middleware;
    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("hotels"));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotels API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddScoped<ISimpleLogger, SimpleLogger>();

            services.AddResponseCaching();

            services.AddMemoryCache();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "hotelsapi";
                });

            //// Redis Cache
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "127.0.0.1";
            //    option.InstanceName = "master";
            //});

            //// SQL Server cache
            //services.AddDistributedSqlServerCache(options =>
            //{
            //    options.ConnectionString = "Data Source=.;Initial Catalog=DistCache;Integrated Security=True;";
            //    options.SchemaName = "dbo";
            //    options.TableName = "TestCache";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                //app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotels API V1"); });
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseResponseCaching();

            //register cache global 
            //app.Use(async (context, next) =>
            //{
            //    context.Response.GetTypedHeaders().CacheControl =
            //        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            //        {
            //            Public = true,
            //            MaxAge = TimeSpan.FromSeconds(120)
            //        };
            //    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =  new string[] { "Accept-Encoding" };

            //    await next();
            //});

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RequestLoggerMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
