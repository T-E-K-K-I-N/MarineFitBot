using MarineFitBot.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarineFitBot.Domain.Entities;

/// <summary>
/// Сущность, представляющая тренировку
/// </summary>
[Table("trainings")]
public class TrainingEntity
{
	/// <summary>
	/// Идентификатор тренировки
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Дата и время тренировки
	/// </summary>
	public DateTime Date { get; set; }

	/// <summary>
	/// Статус тренировки
	/// </summary>
	public TrainingStatus Status { get; set; }

	/// <summary>
	/// Идентификатор пользователя, записанного на тренировку
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// Рекомендации администратора по тренировке
	/// </summary>
	public string? Recommendations { get; set; }

	/// <summary>
	/// Foreign Key
	/// </summary>
	public virtual UserEntity Users { get; set; }
}