using MarineFitBot.Domain.Entities;
using MarineFitBot.Domain.Enums;
using MarineFitBot.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineFitBot.Infra.Data.Repositories
{
    /// <summary>
    /// Репозиторий тренировок 
    /// </summary>
    public class TrainingsRepository : ITrainingsRepository
    {
        private readonly IDbContextFactory<MarineFitBotContext> _dbContextFactory;
        private readonly ILogger<TrainingsRepository> _logger;

        public TrainingsRepository(IDbContextFactory<MarineFitBotContext> dbContextFactory, ILogger<TrainingsRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<TrainingEntity?> CreateAsync(TrainingEntity training, CancellationToken cancellationToken)
        {
            try
            {
                ValidateParams(training);

                if (training.Date == DateTime.MinValue)
                    training.Date = DateTime.Now.AddDays(1).ToUniversalTime();

                if (training.Date < DateTime.Now)
                    throw new InvalidOperationException($"Невозможно создать тренировку, т.к. дата " +
                        $"начала действия не может быть меньше текущей даты");

                training.Date = training.Date.ToUniversalTime();

                using var context = _dbContextFactory.CreateDbContext();

                var existingDate = await context.Trainings.AsNoTracking()
                    .SingleOrDefaultAsync(t => t.Date == training.Date, cancellationToken);


                if (existingDate != null)
                    throw new InvalidOperationException($"Тренировки с таким временем: " +
                        $"'{training.Date.ToString("dd.MM.yyyy HH.mm")}' уже существует.");

                var insertedEntity = context.Trainings.Add(training).Entity;

                await context.SaveChangesAsync(cancellationToken);

                return insertedEntity;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при создании тренировки.");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var existing = await context.Trainings.AsNoTracking()
                    .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

                if (existing == null)
                    throw new InvalidOperationException($"Тренировки с идентификатором: '{id}' не существует.");

                var deletedEntity = context.Trainings.Remove(existing).Entity;

                await context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Тренировка с идентификатором ID: '{id}' удален.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при удалении тренировки.");
            }
        }

        /// <inheritdoc />
        public async Task<List<TrainingEntity>?> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var result = await context.Trainings.AsNoTracking().ToListAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Произошла ошибка при получении списка всех тренировок.");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<TrainingEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var result = await context.Trainings.AsNoTracking()
                    .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении тренировки.");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<List<TrainingEntity>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var result = await context.Trainings.AsNoTracking()
                    .Where(t => t.UserId == userId).ToListAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении тренировок.");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<List<TrainingEntity>?> GetByStatusAsync(TrainingStatus status, CancellationToken cancellationToken)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var result = await context.Trainings.AsNoTracking()
                    .Where(t => t.Status == status).ToListAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении тренировок.");
                return null;
            }
        }

        /// <inheritdoc />
        public async Task<TrainingEntity?> UpdateAsync(TrainingEntity training, CancellationToken cancellationToken)
        {
            try
            {
                ValidateParams(training);

                if(training.Date == DateTime.MinValue)
                    training.Date = DateTime.Now.AddDays(1).ToUniversalTime();

                if(training.Date < DateTime.Now)
                    throw new InvalidOperationException($"Невозможно обновить тренировку, т.к. дата " +
                        $"начала действия не может быть меньше текущей даты");

                training.Date = training.Date.ToUniversalTime();

                using var context = _dbContextFactory.CreateDbContext();

                var existing = await context.Trainings.AsNoTracking()
                    .SingleOrDefaultAsync(u => u.Id == training.Id, cancellationToken) ??
                    throw new InvalidOperationException($"Тренировки с идентификатором: '{training.Id}' не существует.");

                var existingDate = await context.Trainings.AsNoTracking()
                    .SingleOrDefaultAsync(t => t.Date == training.Date, cancellationToken);

                if (existingDate != null && existingDate.Id != training.Id)
                    throw new InvalidOperationException("Невозможно изменить тренировку, " +
                        "т.к. тренировка с такими данными уже существует.");

                context.Trainings.Entry(existing).State = EntityState.Modified;
                context.Trainings.Entry(existing).CurrentValues.SetValues(training);

                await context.SaveChangesAsync(cancellationToken);

                return training;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при обновлении пользователя.");
                return null;
            }
        }

        /// <summary>
        /// Проверка, что переданные данные заполнены
        /// </summary>
        private static void ValidateParams(TrainingEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
        }
    }
}
