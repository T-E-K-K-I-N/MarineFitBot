using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineFitBot.Infra.Data
{
    public class MarineFitBotContextOptions
    {
        public const string Path = "MarineFitBotDbContext";

        public string ConnectionString { get; set; }
    }
}
