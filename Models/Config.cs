using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    public class Config
    {
        public string Token { get; set; }
        public string EffectsPath { get; set; }
        public string EffectsDisplayTextPath { get; set; }
        public ulong GuildId { get; set; }
        public ulong ChannelId { get; set; }
        public long VotingTimeDurationMs { get; set; }
        public long CooldownDurationMs { get; set; }
        public long EffectDuration { get; set; }
        public bool ShowVotesInGame { get; set; }



        private static Config _instance;

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("Config has not been initialized.");
                }
                return _instance;
            }
        }

        public static void Initialize(string filePath)
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("Config has already been initialized.");
            }

            try
            {
                string json = File.ReadAllText(filePath);
                _instance = JsonSerializer.Deserialize<Config>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration file: {ex.Message}");
            }
        }
    }
}
