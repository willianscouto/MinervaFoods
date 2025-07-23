using Microsoft.AspNetCore.Builder;
using MinervaFoods.IoC.ModuleInitializers;

namespace MinervaFoods.IoC
{
    public static class DependencyResolver
    {
        // Delegate usado para testes
        public static Action<WebApplicationBuilder>? TestOverride { get; set; }

        public static void RegisterDependencies(this WebApplicationBuilder builder)
        {
            if (TestOverride != null)
            {
                TestOverride(builder);
                return;
            }
            new ApplicationModuleInitializer().Initialize(builder);
            new InfrastructureModuleInitializer().Initialize(builder);
            new WebApiModuleInitializer().Initialize(builder);
        }
        public static void RegisterDependencies(this WebApplicationBuilder builder,IModuleInitializer[] initializers)
        {
            foreach (var initializer in initializers)
                initializer.Initialize(builder);
        }

    }
}