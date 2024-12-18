using MarineFitBot.Domain.Entities;
using MarineFitBot.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarineFitBot.Infra.Data.Repositories;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public class UsersRepository : IUsersRepository
{
    private readonly IDbContextFactory<MarineFitBotContext> _dbContextFactory;
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(IDbContextFactory<MarineFitBotContext> dbContextFactory, ILogger<UsersRepository> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<UserEntity?> CreateAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            ValidateParams(user);

            using var context = _dbContextFactory.CreateDbContext();

            var existingFullName = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.FullName == user.FullName, cancellationToken);

            var existingTelegramName = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.TelegramName == user.TelegramName, cancellationToken);

            if (existingFullName != null || existingTelegramName != null)
                throw new InvalidOperationException($"Пользователя с таким именем пользователя: '{user.FullName}'" +
                    $" или Telegram-именем (логином): '{user.TelegramName}' уже существует.");

            var insertedEntity = context.Users.Add(user).Entity;

            await context.SaveChangesAsync(cancellationToken);

            return insertedEntity;

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
            using var context = _dbContextFactory.CreateDbContext();

            var existing = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (existing == null)
                throw new InvalidOperationException($"Пользователя с идентификатором: '{id}' не существует.");

            var deletedEntity = context.Users.Remove(existing).Entity;

            await context.SaveChangesAsync(cancellationToken);

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
            using var context = _dbContextFactory.CreateDbContext();

            var result = await context.Users.AsNoTracking().ToListAsync(cancellationToken);

            return result;
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
            using var context = _dbContextFactory.CreateDbContext();

            var result = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

            return result;
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
            using var context = _dbContextFactory.CreateDbContext();

            var result = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.FullName == fullName, cancellationToken);

            return result;
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
            using var context = _dbContextFactory.CreateDbContext();

            var result = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.TelegramName == telegramName, cancellationToken);

            return result;
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

            using var context = _dbContextFactory.CreateDbContext();

            var existing = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == user.Id, cancellationToken) ??
                throw new InvalidOperationException($"Пользователя с идентификатором: '{user.Id}' не существует.");

            var existingFullName = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.FullName == user.FullName, cancellationToken);

            var existingTelegramName = await context.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.TelegramName == user.TelegramName, cancellationToken);

            if ((existingFullName != null && existingFullName.Id != user.Id) ||
                (existingTelegramName != null && existingTelegramName.Id != user.Id))
                throw new InvalidOperationException("Невозможно изменить пользователя, " +
                    "т.к. пользователь с такими данными уже существует.");

            context.Users.Entry(existing).State = EntityState.Modified;
            context.Users.Entry(existing).CurrentValues.SetValues(user);

            await context.SaveChangesAsync(cancellationToken);

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


