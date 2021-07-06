using System;
using System.Collections.Generic;
using System.Text;

namespace FTX.Net.Objects
{
    internal class FTXResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public string Error { get; set; }
    }
}
