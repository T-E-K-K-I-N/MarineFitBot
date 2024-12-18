namespace MarineFitBot.WebApi.Entities
{
    public class TelegramOptions
    {
        public const string Telegram = nameof(Telegram);
        public string BotToken { get; set; } = string.Empty;
    }
}
