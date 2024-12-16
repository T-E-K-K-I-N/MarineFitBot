using MarineFitBot.Api.Entities;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace MarineFitBot.Api.BackgroundServices;

public class TelegramBotBackgroundService : BackgroundService
{
    private readonly ILogger<TelegramBotBackgroundService> _logger;
    private readonly TelegramOptions _telegramOptions;

    public TelegramBotBackgroundService(ILogger<TelegramBotBackgroundService> logger, IOptions<TelegramOptions> telegramOptions)
    {
        _logger = logger;
        _telegramOptions = telegramOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        var botClient = new TelegramBotClient(_telegramOptions.BotToken, cancellationToken: cts.Token);
        
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = []
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            //await botClient.ReceiveAsync(updateHandler: HandleUpdateAsync, pollingErrorHandler: )
        }
    }
}