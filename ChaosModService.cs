using Discord;
using Discord.Net.WebSockets;
using Discord.WebSocket;
using GTA_SA_Chaos_Mod_Discord.Models;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Timers;
using System.Xml.Linq;
using System.Data;
using Discord.Rest;
using System;
using Newtonsoft.Json;

namespace GTA_SA_Chaos_Mod_Discord
{
    public class ChaosModService
    {
        private ChaosWebSocketServer _server;
        private DiscordSocketClient _discordSocketClient;

        private System.Timers.Timer effectTimeTimer;
        private System.Timers.Timer votingCooldownTimer;

        private bool _showVotesInGame;
        private long maxVotingTimeDuration;
        private long votingTimeDuration;
        private long pickedCooldownDuration;

        private long maxCooldownTimeDuration;
        private long cooldownDuration;
        private long effectDuration;

        private Votes votes;
        private bool _sentVote = false;
        private List<effect> _enabledEffects;
        private RestUserMessage discordMsg;
        private static Random rnd;
        public ChaosModService(DiscordSocketClient discordSocketClient)
        {
            _enabledEffects = new List<effect>();
            rnd = new Random();
            cooldownDuration = Config.Instance.CooldownDurationMs;
            votingTimeDuration = Config.Instance.VotingTimeDurationMs;
            _showVotesInGame = Config.Instance.ShowVotesInGame;
            pickedCooldownDuration = cooldownDuration;
            maxCooldownTimeDuration = cooldownDuration;
            maxVotingTimeDuration = votingTimeDuration;


            _discordSocketClient = discordSocketClient;
            _server = new ChaosWebSocketServer(IPAddress.Any, 42069);

            //Load enabled effects
            LoadEffects();

        }
        public async Task Run()
        {

            while (true)
            {
                try
                {
                    if (_server.Start())
                    {
                        break;
                    }
                } 
                catch(Exception ex)
                {
                    Console.WriteLine("Couldn't start WebSocket Server, you most likely have Chaos Launcher opened, either close it or change Chaos GUI port. Waiting 10s");
                    await Task.Delay(10000);
                }

            }

            while (_server.ConnectedSessions == 0)
            {
                Console.WriteLine("Chaos client not connected (Start GTA!), waiting 5s");
                await Task.Delay(5000);
            }

            Console.WriteLine("All set, Started!");
            _discordSocketClient.ReactionAdded += OnReactionAdded;
            _discordSocketClient.ReactionRemoved += OnReactionRemoved;
            votes = new Votes();

            votingCooldownTimer = new System.Timers.Timer(10);
            votingCooldownTimer.AutoReset = false;
            votingCooldownTimer.Elapsed += new ElapsedEventHandler(votingCooldown);
            votingCooldownTimer.Enabled = true;

            effectTimeTimer = new System.Timers.Timer(10);
            effectTimeTimer.AutoReset = false;
            effectTimeTimer.Elapsed += new ElapsedEventHandler(votingTime);
            effectTimeTimer.Enabled = false;
        }

        private Task OnReactionRemoved(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
        {
            if(discordMsg.Id == arg1.Id)
            {
                if (arg3.Emote.Name == "1️⃣")
                {
                    votes.VotesData[0] = votes.VotesData[0] - 1;
                }
                else if (arg3.Emote.Name == "2️⃣")
                {
                    votes.VotesData[1] = votes.VotesData[1] - 1;
                }
                else if (arg3.Emote.Name == "3️⃣")
                {
                    votes.VotesData[2] = votes.VotesData[2] - 1;
                }
                else
                {
                    Console.WriteLine("Voted something else???");
                }
            }

            return Task.CompletedTask;
        }

        private Task OnReactionAdded(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
        {
            if (discordMsg.Id == arg1.Id)
            {
                if (arg3.Emote.Name == "1️⃣")
                {
                    votes.VotesData[0] = votes.VotesData[0] + 1;
                }
                else if (arg3.Emote.Name == "2️⃣")
                {
                    votes.VotesData[1] = votes.VotesData[1] + 1;
                }
                else if (arg3.Emote.Name == "3️⃣")
                {
                    votes.VotesData[2] = votes.VotesData[2] + 1;
                }
                else
                {
                    Console.WriteLine("Votes something else");
                }
            }

            return Task.CompletedTask;
        }

        private async void votingTime(object source, ElapsedEventArgs e) 
        {
            try
            {
                votingTimeDuration -= 10;

                if (!_sentVote)
                {
                    _sentVote = true;
                    //Send vote in discord
                    var effects = GetRandomEffects(3);

                    votes.Effects = effects;
                    votes.EffectsIds = new List<string> { effects[0].EffectId, effects[1].EffectId, effects[2].EffectId };

                    var embed = new EmbedBuilder
                    {
                        Title = "Vote"
                    };


                    embed.AddField("1️⃣", effects[0].DisplayName);
                    embed.AddField("2️⃣", effects[1].DisplayName);
                    embed.AddField("3️⃣", effects[2].DisplayName);
                    discordMsg = await _discordSocketClient.GetGuild(Config.Instance.GuildId).GetTextChannel(Config.Instance.ChannelId).SendMessageAsync(embed: embed.Build());

                    await discordMsg.AddReactionAsync(new Emoji("1️⃣"));
                    await discordMsg.AddReactionAsync(new Emoji("2️⃣"));
                    await discordMsg.AddReactionAsync(new Emoji("3️⃣"));
                }

                if (votingTimeDuration <= 0)
                {
                    
                    await discordMsg.DeleteAsync();
                    discordMsg = null;
                    effect pickedEffect = votes.Effects[votes.VotesData.IndexOf(votes.VotesData.Max())];
                    //change cooldowns to Cooldown time
                    cooldownDuration = pickedCooldownDuration;
                    maxCooldownTimeDuration = pickedCooldownDuration;
                    votingTimeDuration = maxVotingTimeDuration;

                    _sentVote = false;
                    votes.Clear();
                    var effectDuration = Config.Instance.EffectDuration == -1 ? rnd.Next(10000, 61000) : Config.Instance.EffectDuration;
                    pickedEffect.Duration = effectDuration;
                    var message = System.Text.Json.JsonSerializer.Serialize(new Message<effect>(pickedEffect));
                    _server.MulticastText(message);
                    effectTimeTimer.Stop();
                    votingCooldownTimer.Start();
                    return;
                }

                votes.PickedChoice = eVoteChoice.UNDETERMINED; //VoteCalculator.CalculateVoteChoice(votes.VotesData);

                if (_showVotesInGame)
                {
                    var message = System.Text.Json.JsonSerializer.Serialize(new Message<Votes>(votes));
                    _server.MulticastText(message);
                }
                _server.MulticastText(System.Text.Json.JsonSerializer.Serialize(new Message<Time>(new Time { Remaining = votingTimeDuration, Cooldown = maxVotingTimeDuration, Mode = "" })));
            }
            finally
            {
                if (votingTimeDuration != maxVotingTimeDuration) 
                    effectTimeTimer.Start();
            }
        }

        private async void votingCooldown(object source, ElapsedEventArgs e)
        {
            try
            {
                cooldownDuration -= 10;
                if (cooldownDuration <= 0)
                {
                    cooldownDuration = maxCooldownTimeDuration;
                    votingCooldownTimer.Stop();
                    effectTimeTimer.Start();
                }
                else
                {
                    _server.MulticastText(System.Text.Json.JsonSerializer.Serialize(new Message<Time>(new Time { Remaining = cooldownDuration, Cooldown = maxCooldownTimeDuration, Mode = "" })));
                }
            }
            finally
            {
                if (cooldownDuration != maxCooldownTimeDuration)
                    votingCooldownTimer.Start();
            }

        }

        private void LoadEffects()
        {
            string json = File.ReadAllText(Config.Instance.EffectsPath);
            string displayNamesJson = File.ReadAllText(Config.Instance.EffectsDisplayTextPath);

            EffectsConfig jsonEffects = System.Text.Json.JsonSerializer.Deserialize<EffectsConfig>(json);

            //It's better to have updated/new effects with no DisplayText than loading effects from DisplayTextJson and not having updated/new effects, right?
            var displayNames = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(displayNamesJson);
            foreach (var effect in jsonEffects.EnabledEffects)
            {
                if (effect.Value) //enabled
                {
                    var displayName = displayNames.ContainsKey(effect.Key) ? displayNames[effect.Key] : effect.Key;
                    if (effect.Key.StartsWith("teleport_"))
                    {
                        var posX = 0;
                        var posY = 0;
                        var posZ = 0;
                        if (NormalEffect.Locations.ContainsKey(effect.Key))
                        {
                            var location = NormalEffect.Locations[effect.Key];
                            posX = location.x;
                            posY = location.y;
                            posZ = location.z;
                        }

                        _enabledEffects.Add(new NormalEffect("effect_teleport", displayName, effectDuration, new NormalEffect.TeleportEffectEffectData(posX, posY, posZ)));
                    }
                    else if (effect.Key.StartsWith("weather_"))
                    {
                        var weatherId = int.Parse(effect.Key.Split('_')[1]);
                        _enabledEffects.Add(new NormalEffect("effect_weather", displayName, effectDuration, new NormalEffect.WeatherEffectEffectData(weatherId)));
                    } else if(effect.Key.StartsWith("spawn_vehicle"))
                    {
                        var vehicleId = int.Parse(effect.Key.Split('_')[2]);
                        _enabledEffects.Add(new NormalEffect("effect_spawn_vehicle", displayName, effectDuration, new NormalEffect.VehicleEffectEffectData(vehicleId)));
                    }
                    else
                    {
                        _enabledEffects.Add(new NormalEffect(effect.Key, displayName, effectDuration, new NormalEffect.StandardEffectEffectData()));
                    }
                }
            }
            Console.WriteLine($"Loaded {_enabledEffects.Count} effects");
        }

        private List<effect> GetRandomEffects(int count)
        {
            return _enabledEffects.OrderBy(e => rnd.Next()).Take(count).ToList();
        }
    }
}   
