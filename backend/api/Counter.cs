using Newtonsoft.Json;

namespace Company.Function {
    // Creating the Counter Class with Id Count and Time as properties
    public class Counter {
        [JsonProperty(PropertyName="id")] public string Id {get; set;}
        [JsonProperty(PropertyName="count")] public int Count {get; set;}
        [JsonProperty(PropertyName="time")] public string Time {get; set;}
    }
}