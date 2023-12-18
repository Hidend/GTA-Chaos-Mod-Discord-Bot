
# GTA-Chaos-Mod-Discord-Bot

Hi! I needed an updated Discord Bot for [GTA Chaos Mod](https://github.com/gta-chaos-mod/Trilogy-ASI-Script), since I found none working, I made my own kinda fast so this is mess tbh, there may be some bugs and spaghetti code everywhere too but hey, it works™

## Config:
You need a json called **chaosDiscordConfig.cfg** with these settings:
### chaosDiscordConfig.cfg
    {
    "Token": "",
    "EffectsPath": "D:\\Games\\Grand Theft Auto San Andreas\\config.cfg",
    "EffectsDisplayTextPath": "D:\\Games\\Grand Theft Auto San Andreas\\effectsdisplaytext.cfg",
    "GuildId": 11111111111,
    "ChannelId": 11111111111111,
    "VotingTimeDurationMs": 30000,
    "CooldownDurationMs": 30000,
    "EffectDuration": -1,
    "ShowVotesInGame": false
}
Token: [Discord Bot Token](https://docs.discordbotstudio.org/setting-up-dbs/finding-your-bot-token)
EffectsPath: Path to config.cfg of Chaos Mod, we get effects from there, so if you update Chaos Mod, effects will be updated too!
GuildId: Id of the server you will be sending the vote message (Right click on the server image and click copy id)
ChannelId: Id of the channel you will be sending the vote message (Right click the channel and click copy id)
VotingTimeDurationMs: Vote time in milliseconds, basically the time you allow the vote be active
CooldownDurationMs: Cooldown time in milliseconds, basically the time you wait until you ask a new vote.30000, EffectDuration: -1 for a random number between 10 and 60s, if there's an static value like 25000, then every effect will stay for 25s
ShowVotesInGame; If false, you won't see the votes people make on Discord in-game, if true, you will.

### effectsdisplaytext.cfg
It's a file that contains all the effects display text, it's the quickest way I saw to get updated effects without hardcoding all effects on a file (which would mean you don't get updated effects!), so you are free to change all display texts, if somehow I don't find an effect display text, i will just use the key:

        "effect_aggressive_drivers":"Aggressive Drivers"
^For the effect "effect_aggressive_drivers", I will shown on Discord "Aggressive Drivers", you can check all effect Ids [here](https://bit.ly/gta-sa-chaos-mod)

If somehow Chaos Mod added a new effect called "**effect_super_aggresive_drivers**", I will load it from **config.cfg** and since it's not on **effectsdisplaytext.cfg**, people will vote on Discord for **effect_super_aggresive_drivers** and the effect will just work perfectly fine. You can obviously add it on  effectsdisplaytext.cfg and show any display text you want. 

## General info

 - Since we load the effects from config.cfg of Chaos Mod, you can open "Trilogy Chaos Mod.exe", edit the effects you want and close it. You can't have "Trilogy Chaos Mod.exe" opened at the same time as "Discord Chaos Mod".
 - If you don't have Gta Sa game opened, it will advice you to do so.
 
 ## To-do:
 
 - If you voted for  an effect and you vote for another one, make it delete from the first one and vote the another.
 - Learn to code

 ## Thanks
 Thanks to @[Lordmau5](https://github.com/Lordmau5) for Chaos Mod and all the help on Discord!