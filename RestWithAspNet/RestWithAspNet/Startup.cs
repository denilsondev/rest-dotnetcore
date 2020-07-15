using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWithAspNet.Business;
using RestWithAspNet.Business.Implementations;
using RestWithAspNet.Model.Context;
using Microsoft.EntityFrameworkCore;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Implementations;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using RestWithAspNet.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using RestWithAspNet.Hypermedia;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithAspNet
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Rest Api With ASP.NET Core",
                    Version = "v1"
                });
            });


            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnetcion = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve("evolve.json", evolveConnetcion, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
                catch (System.Exception ex)
                {
                    _logger.LogCritical("Database migration failed");

                    throw ex;
                }

            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBookBusiness, BookBusiness>();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id}");
            });
        }
    }
}
