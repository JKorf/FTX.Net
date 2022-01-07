using FTX.Net.Clients;
using FTX.Net.Enums;
using FTX.Net.Interfaces.Clients;
using FTX.Net.Objects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace FTX.Net
{
    /// <summary>
    /// FTX helpers
    /// </summary>
    public static class FTXHelpers
    {
        /// <summary>
        /// Add the IFTXClient and IFTXSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultOptionsCallback">Set default options for the client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IFTXSocketClient for the service collection. Defaults to Scoped.</param>
        /// <returns></returns>
        public static IServiceCollection AddFTX(
            this IServiceCollection services, 
            Action<FTXClientOptions, FTXSocketClientOptions>? defaultOptionsCallback = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            if (defaultOptionsCallback != null)
            {
                var options = new FTXClientOptions();
                var socketOptions = new FTXSocketClientOptions();
                defaultOptionsCallback?.Invoke(options, socketOptions);

                FTXClient.SetDefaultOptions(options);
                FTXSocketClient.SetDefaultOptions(socketOptions);
            }

            services.AddTransient<IFTXClient, FTXClient>();
            if (socketClientLifeTime == null)
                services.AddScoped<IFTXSocketClient, FTXSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IFTXSocketClient), typeof(FTXSocketClient), socketClientLifeTime.Value));
            return services;
        }

        /// <summary>
        /// Kline interval to seconds
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static int ToSeconds(this KlineInterval interval)
        {
            return interval switch
            {
                KlineInterval.OneMinute => 1 * 60,
                KlineInterval.FiveMinutes => 5 * 60,
                KlineInterval.FifteenMinutes => 15 * 60,
                KlineInterval.OneHour => 1 * 60 * 60,
                KlineInterval.FourHours => 4 * 60 * 60,
                KlineInterval.OneDay => 1 * 24 * 60 * 60,
                KlineInterval.OneWeek => 7 * 24 * 60 * 60,
                _ => 0,
            };
        }
    }
}
