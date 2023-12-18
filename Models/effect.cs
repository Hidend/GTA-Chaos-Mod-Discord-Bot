using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    [DataContract(Name = "effect")]
    public abstract class effect
    {
        [JsonPropertyName("effectID")]
        public string EffectId { get; set; }
        [JsonPropertyName("effectData")] 
        public BaseEffectData EffectData { get; set;}
        [JsonPropertyName("duration")]
        public long Duration { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("subtext")]
        public string Subtext { get; set; }

        public effect(string _effectId, string _displayName, long _duration, BaseEffectData _baseEffect)
        {
            EffectId = _effectId;
            EffectData = _baseEffect;
            Duration = _duration;
            DisplayName = _displayName;
            Subtext = "";
        }
    }
}