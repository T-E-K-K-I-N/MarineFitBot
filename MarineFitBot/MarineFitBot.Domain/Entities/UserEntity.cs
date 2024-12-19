using System.ComponentModel.DataAnnotations.Schema;
using MarineFitBot.Domain.Enums;

namespace MarineFitBot.Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
[Table("users")]
public class UserEntity
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


	public virtual ICollection<TrainingEntity>? Trainings { get; set; }
}