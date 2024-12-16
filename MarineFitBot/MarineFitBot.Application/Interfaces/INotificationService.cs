using System.Threading.Tasks;

namespace MarineFitBot.Application.Interfaces
{
	/// <summary>
	/// Интерфейс для отправки уведомлений
	/// </summary>
	public interface INotificationService
	{
		/// <summary>
		/// Отправить уведомление пользователю
		/// </summary>
		/// <param name="chatId">Идентификатор чата telegram</param>
		/// <param name="message">Отправляемое сообщение</param>
		/// <param name="cancellationToken">Токен отмены</param>
		/// <returns>Отправлено ли уведомление</returns>
		Task<bool> NotifyAsync(string chatId, string message, CancellationToken cancellationToken);
	}
}

