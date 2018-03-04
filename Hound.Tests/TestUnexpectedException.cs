using System;

namespace Hound.Tests
{
    class TestUnexpectedException
        : HoundException
    {
        public TestUnexpectedException(string host, Exception innerException)
            : base (host, "Unexpected Exception", innerException)
        {
        }
    }
}
