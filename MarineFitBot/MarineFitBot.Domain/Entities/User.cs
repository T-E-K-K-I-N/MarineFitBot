using MarineFitBot.Domain.Enums;

namespace MarineFitBot.Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User
{
	/// <summary>
	/// Уникальный идентификатор пользователя
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Имя пользователя Telegram
	/// </summary>
	public string TelegramName { get; set; }

	/// <summary>
	/// Полное имя пользователя
	/// </summary>
	public string FullName { get; set; }

	/// <summary>
	/// Роль пользователя (Администратор или Клиент)
	/// </summary>
	public Role Role { get; set; }

	/// <summary>
	/// Список тренировок, связанных с пользователем
	/// </summary>
	public List<Training> Trainings { get; set; } = new();
}