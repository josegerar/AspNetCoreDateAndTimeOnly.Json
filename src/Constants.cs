namespace AspNetCoreDateAndTimeOnly.Json;
internal struct Constants
{
    internal struct Format
    {
        public const string DateOnly = "yyyy-MM-dd";
        public const string TimeOnly = "HH:mm:ss.FFFFFFF";
        public const string DateTime = "yyyy-MM-ddTHH:mm:ss.fff";
        public const string DateTimeOffset = "yyyy-MM-ddTHH:mm:ss.fff";
    }
}