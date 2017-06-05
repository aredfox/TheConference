using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TheConference.InfoBooth.Core;
using TheConference.InfoBooth.Data;

namespace TheConference.Shared.Web.RestApi {
    public partial class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            // Add framework services.                        
            services.AddMediatR(
                typeof(Startup),
                typeof(IInfoBoothContext), typeof(InfoBoothContext)
            );
            services.AddMvc();

            // DI AutoFac
            return BuildAutofacServiceProvider(services);
        }
    }
}
