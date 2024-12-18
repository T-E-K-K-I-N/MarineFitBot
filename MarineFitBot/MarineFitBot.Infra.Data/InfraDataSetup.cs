using MarineFitBot.Domain.Interfaces.Repositories;
using MarineFitBot.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineFitBot.Infra.Data
{
    public static class InfraDataSetup
    {
        public static void AddInfraDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection(MarineFitBotContextOptions.Path).Get<MarineFitBotContextOptions>();

            services.AddMarineFitDbContext(options.ConnectionString);

            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ITrainingsRepository, TrainingsRepository>();
        }

        public static void AddMarineFitDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<MarineFitBotContext>((_, builder) =>
            {
                builder.UseNpgsql(connectionString)
                // Для включения логирования sql в консоль необходимо раскомментировать строку
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            });
        }
    }
}
