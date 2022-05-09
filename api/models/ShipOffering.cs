using Newtonsoft.Json;

namespace api.models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ShipOffering
    {
        [JsonProperty("id")]
        public int ID;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("origin")]
        public Facility Origin;

        [JsonProperty("destination")]
        public Facility Destination;
    }
}