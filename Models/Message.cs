using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GTA_SA_Chaos_Mod_Discord.Models
{
    public class Message<T>
    {
        [JsonPropertyName("type")]
        public string Type { get; }

        [JsonPropertyName("data")]
        public T Data { get; }

        public Message(T data)
        {
            Type = typeof(T).Name.ToLower(); // Automatically assigns the type based on the class name
            Data = data;
        }
    }
}
