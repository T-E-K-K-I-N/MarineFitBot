using MarineFitBot.Domain.Entities;

namespace MarineFitBot.Domain.Inretfaces.Repositories;

public interface ITrainingRepository
{
	/// <summary>
	/// Возвращает тренировку по ID
	/// </summary>
	Task<Training?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

	/// <summary>
	/// Возвращает список тренировок пользователя
	/// </summary>
	Task<List<Training>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
	
	/// <summary>
	/// Создает тренировку
	/// </summary>
	Task<Training?> CreateAsync(Training training, CancellationToken cancellationToken);

	/// <summary>
	/// Обновляет тренировку
	/// </summary>
	Task<Training?> UpdateAsync(Training training, CancellationToken cancellationToken);
	
	/// <summary>
	/// Удаляет тренировку
	/// </summary>
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}