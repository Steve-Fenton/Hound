using System;
using System.Runtime.Serialization;

namespace Hound
{
    public class HoundException
        : ApplicationException
    {
        public HoundEventType Severity { get; protected set; }

        public string HostName { get; private set; }

        public HoundException()
            : base()
        {
            HostName = GetMachineName();
        }

        public HoundException(string message)
            : base(message)
        {
            HostName = GetMachineName();
        }

        public HoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            HostName = GetMachineName();
        }

        public HoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            HostName = GetMachineName();
        }

        #region Host name overloads
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
        #endregion

        internal HoundException(Exception innerException) 
            : base ("Unexpected Exception", innerException)
        {
            HostName = GetMachineName();
        }

        protected static string GetMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch
            {
                return "Unknown Machine (Environment.MachineName Failed)";
            }
        }
    }
}