using MarineFitBot.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MarineFitBot.Application.Services
{
    /// <summary>
    /// Сервис для отправки уведомлений через Telegram
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ITelegramBotClient botClient, ILogger<NotificationService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }
        
        /// <inheritdoc />
        public async Task<bool> NotifyAsync(string chatId, string message, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(chatId))
                {
                    throw new ArgumentNullException(nameof(chatId), "Chat ID не может быть пустым.");
                }

                if (string.IsNullOrWhiteSpace(message))
                {
                    throw new ArgumentNullException(nameof(message), "Сообщение не может быть пустым.");
                }
            
                await _botClient.SendMessage(
                    chatId: chatId,
                    text: message,
                    parseMode: ParseMode.Html, // Поддержка HTML-разметки
                    cancellationToken: cancellationToken
                );
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отправке уведомления пользователю.");
                return false;
            }
        }
    }
}