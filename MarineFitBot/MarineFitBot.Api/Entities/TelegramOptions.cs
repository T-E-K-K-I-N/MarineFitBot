using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineFitBot.Api.Entities
{
    public class TelegramOptions
    {
        public const string Telegram = nameof(Telegram);
        public string BotToken { get; set; }
    }
}
