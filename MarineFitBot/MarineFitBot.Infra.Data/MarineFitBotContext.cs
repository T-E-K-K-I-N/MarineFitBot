using System.Reflection;
using MarineFitBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarineFitBot.Infra.Data;

public class MarineFitBotContext : DbContext
{
	public MarineFitBotContext() {}

	public MarineFitBotContext(DbContextOptions<MarineFitBotContext> options)
		: base(options)
	{
		
	}

	public virtual DbSet<UserEntity> Users { get; set; }
	public virtual DbSet<TrainingEntity> Trainings { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

    // TODO Часть нужна в момент создания миграции
    // Add-Migration Initial_Tables -Project MarineFitBot.Infra.Data
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionString = "User ID=postgres;Password=Raptor311;Host=localhost;Port=5432;Database=MarineFitDb;Pooling=true;";
			optionsBuilder.UseNpgsql(connectionString);
		}
	}
}