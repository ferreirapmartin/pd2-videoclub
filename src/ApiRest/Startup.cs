using ApiRest.Config;
using ApiRest.Support;
using AutoMapper;
using DataAccess.Configuration;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiRest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlSerializerFormatters();
            services.AddLogging();
            services.AddScoped<IVideoclubDbContext, VideoclubDbContext>();
            services.AddSingleton<StatusHelper, StatusHelper>();

            ConfigAutoMapper(services);
        }

        private static void ConfigAutoMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            mappingConfig.AssertConfigurationIsValid();

            services.AddSingleton(mappingConfig.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IVideoclubDbContext universidadDbContext, ILogger<Startup> logger)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            InitDataBase(universidadDbContext, logger);
        }

        private static void InitDataBase(IVideoclubDbContext universidadDbContext, ILogger<Startup> logger)
        {
            var attemps = 6;
            var isSuccess = false;
            while (!isSuccess && attemps > 0)
            {
                try
                {
                    universidadDbContext.EnsureCreated();
                    isSuccess = true;
                }
                catch (Exception e)
                {
                    Task.Delay(4000).Wait();
                    if (attemps == 0)
                    {
                        logger.LogError(e, "No se pudo inicializar la base de datos");
                        throw;
                    }
                }
                finally
                {
                    attemps--;
                }
            }

            DbInitializer.Initialize(universidadDbContext);

            logger.LogInformation("Se puede inicializar la base de datos correctamente");
        }
    }
}
