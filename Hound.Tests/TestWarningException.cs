namespace Hound.Tests
{
    public class TestWarningException
        : HoundException
    {
        public TestWarningException(string host, string title)
            : base(host, $"The title is {title}")
        {
            Severity = HoundEventType.Warning;
        }
    }
}