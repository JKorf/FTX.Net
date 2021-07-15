namespace FTX.Net.Objects
{
    internal class FTXResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; } = default!;
        public string Error { get; set; } = string.Empty;
    }
}
