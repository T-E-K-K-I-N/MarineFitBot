using MarineFitBot.Domain.Entities;

namespace MarineFitBot.Application.Interfaces;

/// <summary>
/// Интерфейс для работы с тренировками
/// </summary>
public interface ITrainingService
{
	/// <summary>
	/// Получить список тренировок пользователя
	/// </summary>
	/// <param name="userId">Идентификатор пользователя</param>
	/// <returns>Список тренировок</returns>
	Task<List<Training>> GetUserTrainingsAsync(Guid userId);

	/// <summary>
	/// Получить расписание тренировок для администратора
	/// </summary>
	/// <returns>Список тренировок</returns>
	Task<List<Training>> GetAdminScheduleAsync();

	/// <summary>
	/// Создать тренировку для пользователя
	/// </summary>
	/// <param name="userId">Идентификатор пользователя</param>
	/// <param name="date">Дата тренировки</param>
	/// <returns></returns>
	Task CreateTrainingAsync(Guid userId, DateTime date);

	/// <summary>
	/// Подтвердить тренировку
	/// </summary>
	Task ConfirmTrainingAsync(Guid trainingId);

	/// <summary>
	/// Отклонить тренировку
	/// </summary>
	Task DeclineTrainingAsync(Guid trainingId);
}