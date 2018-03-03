using System;
using System.Collections.Generic;

namespace Hound
{
    public class HoundMetricCollection
    {
        /// <summary>
        /// The metric title, used to group events, usually in the format <code>group.item</code>,
        /// for example <code>system.cpu</code> or <code>processqueue.count</code>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The machine, container, or other identity of where the metric occurred
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// A collection of datapoints, recommended to be 15 seconds or more apart
        /// </summary>
        public IEnumerable<HoundMetricPoint> Points { get; set; }
    }

    public class HoundMetricPoint
    {
        /// <summary>
        /// The timestamp for the metric, metrics that are too old, or too far into the future are discarded.
        /// Generally the metric can be up to an hour old. If the timestamp is up to 10 minutes in the future,
        /// it will typically be accepted to account for clock drift.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The value for the metric. You choose the scale, but stick to it once you have decided.
        /// </summary>
        public decimal Value { get; set; }
    }
}