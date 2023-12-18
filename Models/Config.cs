using DiscordBot;
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
                if (!File.Exists(filePath))
                {
                    var defaultConfig = new Config
                    {
                        Token = "YOUR_DISCORD_BOT_TOKEN",
                        EffectsPath = "D:\\Games\\Grand Theft Auto San Andreas\\config.cfg",
                        EffectsDisplayTextPath =  "D:\\Games\\Grand Theft Auto San Andreas\\effectsdisplaytext.cfg",
                        GuildId =  11111111111,
                        ChannelId =  11111111111111,
                        VotingTimeDurationMs = 30000,
                        CooldownDurationMs = 30000,
                        EffectDuration = -1,
                        ShowVotesInGame = false
                        };

                    string defaultConfigJson = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    File.WriteAllText(filePath, defaultConfigJson);

                    Console.WriteLine("Config file created with default values. Exiting so you configure it!");
                    Thread.Sleep(3000);
                    System.Environment.Exit(1);
                }

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
