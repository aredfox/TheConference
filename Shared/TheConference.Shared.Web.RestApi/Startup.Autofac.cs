using System.Collections.Generic;
using System.Reflection;
using Autofac;
using System;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace TheConference.Shared.Web.RestApi {
    public partial class Startup {
        public IContainer ApplicationContainer { get; private set; }
        private readonly IEnumerable<string> _assemblyNamesToScanForModules = new[] {
            Assembly.GetEntryAssembly().GetName().Name,
            "TheConference.InfoBooth.Data"
        };

        private IServiceProvider BuildAutofacServiceProvider(IServiceCollection services) {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(_assemblyNamesToScanForModules.ProjectToAssemblies());
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }
    }

    internal static class StartupArrayOfStringExtensions {
        internal static Assembly[] ProjectToAssemblies(this IEnumerable<string> assemblyNames) {
            return assemblyNames.Select(name => Assembly.Load(new AssemblyName(name))).ToArray();
        }
    }
}
