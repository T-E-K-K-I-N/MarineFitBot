using MarineFitBot.Domain.Entities;

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