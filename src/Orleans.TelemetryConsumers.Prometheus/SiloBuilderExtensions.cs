using Microsoft.Extensions.DependencyInjection;
using Orleans.Configuration;
using Orleans.Hosting;
using System;

namespace Orleans.TelemetryConsumers.Prometheus
{
    public static class SiloBuilderExtensions
    {
        public static ISiloBuilder AddPrometheusTelemetry(this ISiloBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices(x =>
                x.Configure<TelemetryOptions>(options => options.AddConsumer<TelemetryConsumer>()));

            return builder;
        }
    }
}