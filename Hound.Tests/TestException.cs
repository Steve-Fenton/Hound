namespace Hound.Tests
{
    public class TestException
        : HoundException
    {
        public TestException(string host, string author)
            : base(host, $"The author is {author}")
        {
            Severity = HoundEventType.Error;
        }
    }
}