using System;

namespace Hound
{
    public class HoundResult
    {
        /// <summary>
        /// Indicates that the operation completed successfully
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Where appropriate, contains a Uri for the published item
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Exception informatio for failed requests.
        /// As a general rule, these shouldn't be thrown as it would probably cause
        /// calling applications to attempt to pass them back to Hound.
        /// </summary>
        public Exception Error { get; set; }
    }
}