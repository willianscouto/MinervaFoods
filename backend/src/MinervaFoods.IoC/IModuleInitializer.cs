using Microsoft.AspNetCore.Builder;

namespace MinervaFoods.IoC
{
    public interface IModuleInitializer
    {
        void Initialize(WebApplicationBuilder builder);
    }

}
