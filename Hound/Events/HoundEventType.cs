namespace Hound
{
    public enum HoundEventType
    {
        Error,
        Warning,
        Info,
        Success
    }

    public static class EventTypeMapper
    {
        public static string GetString(HoundEventType eventType)
        {
            return eventType.ToString().ToLowerInvariant();
        }
    }
}