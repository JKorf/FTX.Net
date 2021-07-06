using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net
{
    public class FTXOptions : RestClientOptions
    {
        public FTXOptions(): base("https://ftx.com/api")
        {

        }
    }
}
