using System.Collections.Generic;

namespace Hound
{
    public class HoundEvent
    {
        /// <summary>
        /// The main headline for the event
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The details of the event
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The machine, container, or other identity of where the event occurred
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// A list of tags to apply to the event
        /// </summary>
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        /// The severity of the event
        /// </summary>
        public HoundEventType AlertType { get; set; }

        /// <summary>
        /// A key that links multiple similar events, allowing them to be grouped
        /// </summary>
        public string AggregationKey { get; set; }

        internal string SourceTypeName { get; private set; } = "hound";
    }
}
