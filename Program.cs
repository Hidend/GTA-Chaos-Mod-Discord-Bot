using Discord;
using Discord.WebSocket;
using GTA_SA_Chaos_Mod_Discord;
using GTA_SA_Chaos_Mod_Discord.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace DiscordBot
{
    public class Program
    {
        private readonly DiscordSocketClient _client;
        private readonly ChaosModService _chaosModService;
        public Program()
        {
               var config = new DiscordSocketConfig()
               {
                   GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
                   AlwaysDownloadUsers = true,
               };

            Config.Initialize("chaosDiscordConfig.cfg");

            _client = new DiscordSocketClient(config);
            _chaosModService = new ChaosModService(_client);
            _client.Ready += ReadyAsync;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }
         
        static void Main(string[] args)
            => new Program().RunAsync()
                .GetAwaiter()
                .GetResult();

        public async Task RunAsync()
        {
            await _client.LoginAsync(TokenType.Bot, Config.Instance.Token);
            await _client.StartAsync();

            //I should use ReadyAsync, ik
            await Task.Delay(10000);

            await _chaosModService.Run();
            // Block the program until it is closed.
            await Task.Delay(Timeout.Infinite);
        }
    }
}