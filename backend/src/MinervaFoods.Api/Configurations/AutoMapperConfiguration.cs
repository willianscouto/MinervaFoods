using AutoMapper;
using MinervaFoods.Application;

namespace MinervaFoods.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(Program).Assembly);
                    cfg.AddMaps(typeof(ApplicationLayer).Assembly);
                }, loggerFactory);

                //config.AssertConfigurationIsValid(); 

                return config.CreateMapper();
            });
        }
    }
}
