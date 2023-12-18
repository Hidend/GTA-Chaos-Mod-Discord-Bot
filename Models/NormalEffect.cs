using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static GTA_SA_Chaos_Mod_Discord.Models.effect;

namespace GTA_SA_Chaos_Mod_Discord.Models
{

    public class NormalEffect : effect
    {
        public static Dictionary<string, (int x, int y, int z)> Locations = new Dictionary<string, (int x, int y, int z)>
        {
            { "teleport_grove_street", (2493, -1670, 15) },
            { "teleport_a_tower", (1544, -1353, 332) },
            { "teleport_a_pier", (836, -2061, 15) },
            { "teleport_the_ls_airport", (2109, -2544, 16) },
            { "teleport_the_docks", (2760, -2456, 16) },
            { "teleport_a_mountain", (-2233, -1737, 483) },
            { "teleport_the_sf_airport", (-1083, 409, 17) },
            { "teleport_a_bridge", (-2669, 1595, 220) },
            { "teleport_a_secret_place", (213, 1911, 20) },
            { "teleport_a_quarry", (614, 856, -40) },
            { "teleport_the_lv_airport", (1612, 1166, 17) },
            { "teleport_big_ear", (-310, 1524, 78) }
        };

        public NormalEffect(string _effectId, string _displayName, long _duration, BaseEffectData _baseEffect) : base(_effectId, _displayName, _duration, _baseEffect)
        {

        }

        public class StandardEffectEffectData : BaseEffectData
        {
            [JsonProperty("seed")]
            public int seed { get; set; }
            public StandardEffectEffectData()
            {
                
                seed = 0;
            }
        }

        public class WeatherEffectEffectData : BaseEffectData
        {
            public int weatherID { get; set; }
            public WeatherEffectEffectData(int _weatherID)
            {
                weatherID = _weatherID;
            }
        }

        public class TeleportEffectEffectData : BaseEffectData
        {
            [JsonProperty("posX")]
            public int posX { get; set; }

            [JsonProperty("posY")]
            public int posY { get; set; }

            [JsonProperty("posZ")]
            public int posZ { get; set; }

            public TeleportEffectEffectData(int _posX, int _posY, int _posZ)
            {
                this.posX = _posX;
                this.posY = _posY;
                this.posZ = _posZ;
            }
        }

        public class VehicleEffectEffectData : BaseEffectData
        {
            [JsonProperty("vehicleID")]
            public int vehicleID { get; set; }
            public VehicleEffectEffectData(int _vehicleId)
            {
                this.vehicleID = _vehicleId;
            }
        }
    }
}
