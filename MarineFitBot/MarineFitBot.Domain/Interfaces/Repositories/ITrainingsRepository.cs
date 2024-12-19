using MarineFitBot.Domain.Entities;
using MarineFitBot.Domain.Enums;

namespace MarineFitBot.Domain.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория тренировок
/// </summary>
public interface ITrainingsRepository
{
    /// <summary>
    /// Возвращает список всех тренировок
    /// </summary>
    Task<List<TrainingEntity>?> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает тренировку по ID
    /// </summary>
    Task<TrainingEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

	/// <summary>
	/// Возвращает список тренировок пользователя
	/// </summary>
	Task<List<TrainingEntity>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает список тренировок в конкретном статусе
    /// </summary>
    Task<List<TrainingEntity>?> GetByStatusAsync(TrainingStatus status, CancellationToken cancellationToken);

    /// <summary>
    /// Создает тренировку
    /// </summary>
    Task<TrainingEntity?> CreateAsync(TrainingEntity training, CancellationToken cancellationToken);

	/// <summary>
	/// Обновляет тренировку
	/// </summary>
	Task<TrainingEntity?> UpdateAsync(TrainingEntity training, CancellationToken cancellationToken);
	
	/// <summary>
	/// Удаляет тренировку
	/// </summary>
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}