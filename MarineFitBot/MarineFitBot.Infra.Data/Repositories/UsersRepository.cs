using MarineFitBot.Domain.Entities;
using MarineFitBot.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarineFitBot.Infra.Data.Repositories;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public class UsersRepository : RepositoryBase<UserEntity>, IUsersRepository
{
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(IDbContextFactory<MarineFitBotContext> dbContextFactory, ILogger<UsersRepository> logger)
        : base(dbContextFactory)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<UserEntity?> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            ValidateParams(user);

            var existingFullName = await FindSingleByConditionAsync(u => 
                u.FullName == user.FullName, cancellationToken);

            var existingTelegramName = await FindSingleByConditionAsync(u => 
                u.TelegramName == user.TelegramName, cancellationToken);

            if (existingFullName != null || existingTelegramName != null)
                throw new InvalidOperationException($"Пользователя с таким именем пользователя: '{user.FullName}'" +
                    $" или Telegram-именем (логином): '{user.TelegramName}' уже существует.");

            await InsertEntityAsync(user, cancellationToken);

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при создании пользователя.");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var existing = await FindByIdAsync(id, cancellationToken);

            if (existing == null)
                throw new InvalidOperationException($"Пользователя с идентификатором: '{id}' не существует.");

           await DeleteEntityAsync(existing, cancellationToken);

            _logger.LogInformation($"Пользователь с идентификатором ID: '{id}' удален.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при удалении пользователя.");
        }
    }

    /// <inheritdoc />
    public async Task<List<UserEntity>?> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await FindAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при получении списка всех пользователей.");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await FindByIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при получении пользователя.");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<UserEntity?> GetByFullNameAsync(string fullName, CancellationToken cancellationToken)
    {
        try
        {
            return await FindSingleByConditionAsync(u => u.FullName == fullName, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при получении пользователя.");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<UserEntity?> GetByTelegramNameAsync(string telegramName, CancellationToken cancellationToken)
    {
        try
        {
            return await FindSingleByConditionAsync(u => u.TelegramName == telegramName, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при получении пользователя.");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<UserEntity?> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            ValidateParams(user);

            var existing = await FindByIdAsync(user.Id, cancellationToken) ??
                throw new InvalidOperationException($"Пользователя с идентификатором: '{user.Id}' не существует.");

            var existingFullName = await FindSingleByConditionAsync(u =>
                u.FullName == user.FullName, cancellationToken);

            var existingTelegramName = await FindSingleByConditionAsync(u =>
                u.TelegramName == user.TelegramName, cancellationToken);

            if ((existingFullName != null && existingFullName.Id != user.Id) ||
                (existingTelegramName != null && existingTelegramName.Id != user.Id))
                throw new InvalidOperationException("Невозможно изменить пользователя, " +
                    "т.к. пользователь с такими данными уже существует.");

            await UpdateEntityAsync(user, cancellationToken);

            return user;
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
    private static void ValidateParams(UserEntity entity)
    {
        if (entity is null) 
            throw new ArgumentNullException(nameof(entity));

        if(string.IsNullOrEmpty(entity.FullName))
            throw new ArgumentException(nameof(entity.FullName), "Поле 'FullName' не может быть пустым или равным null.");

        if (string.IsNullOrEmpty(entity.TelegramName))
            throw new ArgumentException(nameof(entity.FullName), "Поле 'TelegramName' не может быть пустым или равным null.");
    }
}


