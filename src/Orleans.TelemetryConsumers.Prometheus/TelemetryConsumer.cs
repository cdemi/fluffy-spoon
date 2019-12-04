using Orleans.Runtime;
using Prometheus;
using System;
using System.Collections.Generic;

namespace Orleans.TelemetryConsumers.Prometheus
{
    public class TelemetryConsumer : IDependencyTelemetryConsumer, IMetricTelemetryConsumer, IRequestTelemetryConsumer
    {
        private static readonly Dictionary<string, Collector> Observers = new Dictionary<string, Collector>();

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            var gauge = AddOrGetObserver(name, (x) => Metrics.CreateGauge(x, name));

            gauge.Set(value);
        }

        public void TrackMetric(string name, TimeSpan value, IDictionary<string, string> properties = null)
        {
            var summary = AddOrGetObserver(name, (x) => Metrics.CreateSummary(x, name));

            summary.Observe(value.TotalSeconds);
        }

        public void IncrementMetric(string name)
        {
            var gauge = AddOrGetObserver(name, (x) => Metrics.CreateGauge(x, name));

            gauge.Inc();
        }

        public void IncrementMetric(string name, double value)
        {
            var gauge = AddOrGetObserver(name, (x) => Metrics.CreateGauge(x, name));

            gauge.Inc(value);
        }

        public void DecrementMetric(string name)
        {
            var gauge = AddOrGetObserver(name, (x) => Metrics.CreateGauge(x, name));

            gauge.Dec(1);
        }

        public void DecrementMetric(string name, double value)
        {
            var gauge = AddOrGetObserver(name, (x) => Metrics.CreateGauge(x, name));

            gauge.Dec();
        }

        public void TrackRequest(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
        {
            var summary = AddOrGetObserver(name, (x) => Metrics.CreateSummary(x, name));

            summary.Observe(duration.TotalSeconds);
        }

        public void TrackDependency(string dependencyName, string commandName, DateTimeOffset startTime, TimeSpan duration,
            bool success)
        {
            var summary = AddOrGetObserver(dependencyName, (x) => Metrics.CreateSummary(x, dependencyName));

            summary.Observe(duration.TotalSeconds);
        }
        
        public void Flush()
        {
        }

        public void Close()
        {
        }
        
        private static T AddOrGetObserver<T>(string name, Func<string, T> createAction) where T : Collector
        {
            // TODO: Optimistic Locking

            if (!Observers.TryGetValue(name, out var observer))
            {
                observer = createAction(FormatMetricName(name));

                Observers.Add(name, observer);
            }

            return (T) observer;
        }

        private static string FormatMetricName(string name)
        {
            return name.ToLower()
                .Replace(".", "_")
                .Replace("-", "_")
                .Replace("/", "_");
        }
    }
}