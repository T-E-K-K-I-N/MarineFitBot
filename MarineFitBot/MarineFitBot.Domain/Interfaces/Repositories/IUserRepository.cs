using MarineFitBot.Domain.Entities;

namespace MarineFitBot.Domain.Inretfaces.Repositories;

public interface IUserRepository
{
	/// <summary>
	/// Возвращает пользователя по ID
	/// </summary>
	Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	
	/// <summary>
	/// Возвращает пользователя по имени(логину) Telegram
	/// </summary>
	Task<User?> GetByTelegramIdAsync(string telegramName, CancellationToken cancellationToken);
	
	/// <summary>
	/// Создает пользователя
	/// </summary>
	Task<User?> CreateAsync(User training, CancellationToken cancellationToken);
	
	/// <summary>
	/// Обновляет пользователя
	/// </summary>
	Task<User?> UpdateAsync(User training, CancellationToken cancellationToken);
	
	/// <summary>
	/// Удаляет тренировку
	/// </summary>
	Task DeleteAsync(Training training, CancellationToken cancellationToken);
}