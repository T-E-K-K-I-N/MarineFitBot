namespace MarineFitBot.Domain.Enums;

/// <summary>
/// Статусы тренировок
/// </summary>
public enum TrainingStatus
{
	Pending = 0,   // Ожидает подтверждения
	Confirmed = 1, // Подтверждена
	Declined = 2   // Отклонена
}