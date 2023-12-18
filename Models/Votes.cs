using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    [DataContract(Name = "votes")]
    public class Votes
    {
        [JsonPropertyName("effects")]
        public List<string> EffectsIds { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<effect> Effects { get; set; }

        [JsonPropertyName("votes")]
        public List<int> VotesData { get; set; }

        [JsonPropertyName("pickedChoice")]
        public eVoteChoice PickedChoice { get; set; }

        public Votes() 
        {
            EffectsIds = new List<string>();
            VotesData = new List<int> { 0, 0, 0};
            PickedChoice = eVoteChoice.NONE;
        }

        public void Clear()
        {
            EffectsIds = new List<string>();
            VotesData = new List<int> { 0, 0, 0 };
            PickedChoice = eVoteChoice.NONE;
        }
    }
}
