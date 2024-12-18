using MarineFitBot.Domain;
using MarineFitBot.Infra.Data;
using MarineFitBot.WebApi.BackgroundServices;
using MarineFitBot.WebApi.Entities;

namespace MarineFitBot.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            ConfigureServices(builder.Services, configuration);

            var app = builder.Build();

            ConfigureApp(app, configuration);

            app.Run();
        }

        /// <summary>
        /// Конфигурация сервисов
        /// </summary>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDomainLayer();
            services.AddInfraDataLayer(configuration);

            services.AddHostedService<TelegramBotBackgroundService>();

            services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.Telegram));
        }

        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private static void ConfigureApp(WebApplication app, IConfiguration configuration)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
