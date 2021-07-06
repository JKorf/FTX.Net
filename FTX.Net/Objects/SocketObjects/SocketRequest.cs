using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
