using System;
using System.Runtime.Serialization;

namespace Hound
{
    public class HoundException
        : ApplicationException
    {
        public HoundEventType Severity { get; protected set; }

        public string HostName { get; private set; }

        public HoundException(string host)
            : base()
        {
            HostName = host;
        }

        public HoundException(string host, string message)
            : base(message)
        {
            HostName = host;
        }

        public HoundException(string host, string message, Exception innerException)
            : base(message, innerException)
        {
            HostName = host;
        }

        public HoundException(string host, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            HostName = host;
        }
    }
}