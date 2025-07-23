using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinervaFoods.Application.Cotacao.Moeda;
using MinervaFoods.Data;
using MinervaFoods.Data.Repositories;
using MinervaFoods.Domain.Repositories;
using MinervaFoods.Helpers.Http;
using MinervaFoods.Helpers.Security;

namespace MinervaFoods.IoC.ModuleInitializers
{
    public class InfrastructureModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserAuthenticate, HttpContextUserAuthenticate>();
            builder.Services.AddScoped<IHttpRequestClient, HttpRequestClient>();
            builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
            builder.Services.AddScoped<ICotacaoMoedaService, CotacaoMoedaService>();
            builder.Services.AddScoped<ICarneRepository, CarneRepository>();
            builder.Services.AddScoped<ICompradorRepository, CompradorRepository>();
            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
            builder.Services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();
            builder.Services.AddScoped<IPaisRepository, PaisRepository>();
            builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();

        }
    }
}