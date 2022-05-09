using Newtonsoft.Json;

namespace api.models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Facility
    {   
        [JsonProperty("id")]
        public int ID;

        [JsonProperty("name")]
        public string Name;
    }
}