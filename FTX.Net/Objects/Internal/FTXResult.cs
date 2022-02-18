namespace FTX.Net.Objects.Internal
{
    internal class FTXResult
    {
        public bool Success { get; set; }
        public string Error { get; set; } = string.Empty;
    }

    internal class FTXResult<T>: FTXResult
    {
        public T Result { get; set; } = default!;
    }
}
