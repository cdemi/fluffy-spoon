using FluentValidation.AspNetCore;
using fluffyspoon.registration.contracts;
using fluffyspoon.registration.Grains;
using GiG.Core.DistributedTracing.Web.Extensions;
using GiG.Core.HealthChecks.Extensions;
using GiG.Core.Hosting.Extensions;
using GiG.Core.Orleans.Clustering.Consul.Extensions;
using GiG.Core.Orleans.Clustering.Extensions;
using GiG.Core.Orleans.Clustering.Kubernetes.Extensions;
using GiG.Core.Orleans.Silo.Dashboard.Extensions;
using GiG.Core.Orleans.Silo.Extensions;
using GiG.Core.Orleans.Streams.Extensions;
using GiG.Core.Web.Docs.Extensions;
using GiG.Core.Web.FluentValidation.Extensions;
using GiG.Core.Web.Hosting.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using OrleansDashboard;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace fluffyspoon.registration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Info Management
            services.ConfigureInfoManagement(Configuration);

            // Health Checks
            services.ConfigureHealthChecks(Configuration);
            services.AddHealthChecks();

            // Web Api
            services.ConfigureApiDocs(Configuration)
                .AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

            // Forwarded Headers
            services.ConfigureForwardedHeaders();

            // Configure Api Behavior Options
            services.ConfigureApiBehaviorOptions();
            
            // Add Orleans Streams Services
            services.AddStreamFactory();
        }
        
        // This method gets called by the runtime. Use this method to configure Orleans.
        public static void ConfigureOrleans(HostBuilderContext ctx, ISiloBuilder builder)
        {
            var configuration = ctx.Configuration;

            builder.ConfigureCluster(configuration)
                .UseDashboard(x =>
                {
                    x.BasePath = "/dashboard";
                    x.HostSelf = false;
                })
                .ConfigureEndpoints()
                .AddAssemblies(typeof(RegistrationGrain))
                .AddSimpleMessageStreamProvider(Constants.StreamProviderName)
                .AddMemoryGrainStorage("PubSubStore")
                .UseMembershipProvider(configuration, x =>
                {
                    x.ConfigureConsulClustering(configuration);
                    x.ConfigureKubernetesClustering(configuration);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseForwardedHeaders();
            app.UsePathBaseFromConfiguration();
            app.UseCorrelation();
            app.UseRouting();
            app.UseFluentValidationMiddleware();
            app.UseHealthChecks();
            app.UseInfoManagement();
            app.UseApiDocs();
            app.UseOrleansDashboard(new DashboardOptions()
            {
                BasePath = "/dashboard",
                HostSelf = false
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}