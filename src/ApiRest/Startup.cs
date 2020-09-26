using ApiRest.Config;
using ApiRest.Support;
using AutoMapper;
using DataAccess.Configuration;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IVideoclubDbContext universidadDbContext)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            InitDataBase(universidadDbContext);
        }

        private static void InitDataBase(IVideoclubDbContext universidadDbContext)
        {
            universidadDbContext.EnsureCreated();

            DbInitializer.Initialize(universidadDbContext);
        }
    }
}
