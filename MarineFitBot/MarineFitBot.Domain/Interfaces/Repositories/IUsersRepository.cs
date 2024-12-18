using MarineFitBot.Domain.Entities;

namespace MarineFitBot.Domain.Interfaces.Repositories;

/// <summary>
/// Интерфейс репозитория пользователей
/// </summary>
public interface IUsersRepository
{
    /// <summary>
    /// Возвращает список всех пользователей
    /// </summary>
    Task<List<UserEntity>?> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает пользователя по ID
    /// </summary>
    Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает пользователя по имени
    /// </summary>
    Task<UserEntity?> GetByFullNameAsync(string fullName, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает пользователя по имени(логину) Telegram
    /// </summary>
    Task<UserEntity?> GetByTelegramNameAsync(string telegramName, CancellationToken cancellationToken);
	
	/// <summary>
	/// Создает пользователя
	/// </summary>
	Task<UserEntity?> CreateAsync(UserEntity user, CancellationToken cancellationToken);
	
	/// <summary>
	/// Обновляет пользователя
	/// </summary>
	Task<UserEntity?> UpdateAsync(UserEntity user, CancellationToken cancellationToken);
	
	/// <summary>
	/// Удаляет тренировку
	/// </summary>
	Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}