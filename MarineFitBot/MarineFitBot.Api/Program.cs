using System.Globalization;
using Microsoft.Extensions.Configuration;
using MarineFitBot.Api.Entities;
using MarineFitBot.Api.BackgroundServices;
namespace MarineFitBot.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var configuration = builder.Configuration;

            ConfigureServices(builder.Services, configuration);



            var host = builder.Build();
            host.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<TelegramBotBackgroundService>();
            services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.Telegram));
        }


}
}

