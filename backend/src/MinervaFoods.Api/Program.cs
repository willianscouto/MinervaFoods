using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using MinervaFoods.Application;
using MinervaFoods.Data;
using MinervaFoods.Helpers;
using MinervaFoods.IoC;
using Serilog;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using MinervaFoods.Helpers.HealthChecks;
using MinervaFoods.Helpers.Logging;
using MinervaFoods.Api.Configurations;

public class Program
{
    public static void Main(string[] args)
    {

        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.AddDefaultLogging();
            // Add services to the container.

            var defaultCulture = new CultureInfo("pt-BR");
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = new[] { defaultCulture };
                options.SupportedUICultures = new[] { defaultCulture };
            });
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();



            builder.Services.AddSwaggerGen(options =>
            {
                options.MapType<DateTime>(() => new Microsoft.OpenApi.Models.OpenApiSchema
                {
                    Type = "string",
                    Format = "date-time",
                    Example = new OpenApiString("2025-01-01T00:00:00Z")
                });
            });


            builder.Services.AddDbContext<DefaultContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly("MinervaFoods.Data")
               )
           );


            builder.RegisterDependencies();

            builder.Services.AddAutoMapperProfiles();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //  app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly TimeZoneInfo BrasiliaTimeZone =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")
                : TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var date = reader.GetDateTime();


            if (date.Kind == DateTimeKind.Local)
                return date;


            var dateWithKind = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTime(dateWithKind, BrasiliaTimeZone);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {

            var brasiliaTime = TimeZoneInfo.ConvertTimeFromUtc(value.ToUniversalTime(), BrasiliaTimeZone);
            writer.WriteStringValue(brasiliaTime.ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }


}