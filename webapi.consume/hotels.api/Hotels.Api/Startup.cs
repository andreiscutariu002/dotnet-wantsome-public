namespace Hotels.Api
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
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
            services.AddMvc(options =>
                {
                    // Return a 406 when an unsupported media type was requested
                    options.ReturnHttpNotAcceptable = true;

                    // Add XML formatters
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));

                    options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());

                    // Set XML as default format instead of JSON - the first formatter in the 
                    // list is the default, so we insert the input/output formatters at 
                    // position 0
                    //options.OutputFormatters.Insert(0, new XmlSerializerOutputFormatter());
                    //options.InputFormatters.Insert(0, new XmlSerializerInputFormatter(options));
                }
            );

            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Hotels API", Version = "v1"});
            });

            services.AddScoped<ISimpleLogger, SimpleLogger>();

            services.AddResponseCaching();
            
            services.AddMemoryCache();

            //add redis cache
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "127.0.0.1";
            //    option.InstanceName = "master";
            //});

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = "Data Source=.;Initial Catalog=DistCache;Integrated Security=True;";
                options.SchemaName = "dbo";
                options.TableName = "TestCache";
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
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

            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
