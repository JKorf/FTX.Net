namespace FTX.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the FTX clients
    /// </summary>
    public class FTXApiAddresses
    {
        /// <summary>
        /// The address used by the FTXClient for the rest API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the FTXSocketClient for the socket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the FTX.com API
        /// </summary>
        public static FTXApiAddresses Default = new FTXApiAddresses
        {
            RestClientAddress = "https://ftx.com/api",
            SocketClientAddress = "wss://ftx.com/ws/"
        };

        /// <summary>
        /// The addresses to connect to the FTX US API
        /// </summary>
        public static FTXApiAddresses Us = new FTXApiAddresses
        {
            RestClientAddress = "https://ftx.us/api",
            SocketClientAddress = "wss://ftx.us/ws/"
        };
    }
}
