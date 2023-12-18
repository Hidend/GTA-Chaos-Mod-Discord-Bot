using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static GTA_SA_Chaos_Mod_Discord.Models.NormalEffect;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    [JsonDerivedType(typeof(StandardEffectEffectData))]
    [JsonDerivedType(typeof(WeatherEffectEffectData))]
    [JsonDerivedType(typeof(TeleportEffectEffectData))]
    [JsonDerivedType(typeof(VehicleEffectEffectData))]
    [DataContract(Name = "effectData")]
    public class BaseEffectData
    {

    }

}
