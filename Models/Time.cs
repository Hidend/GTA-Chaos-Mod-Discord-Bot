using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    [DataContract(Name = "time")]
    public class Time
    {
        [JsonPropertyName("remaining")]
        public long Remaining { get; set; }

        [JsonPropertyName("cooldown")]
        public long Cooldown { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }
    }
}
