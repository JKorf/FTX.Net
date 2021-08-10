using Newtonsoft.Json;

namespace FTX.Net.Objects.SocketObjects
{
    public class SocketRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }

        public SocketRequest(string operation)
        {
            Operation = operation;
        }
    }
}
