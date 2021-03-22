using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace Redis_Cache_Services.Redis
{
    public class PubSubService : BackgroundService
    {
        private readonly ISubscriber _sub;
        public PubSubService(ISubscriber sub)
        {
           _sub = sub ?? throw new ArgumentNullException(nameof(sub));
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _sub.SubscribeAsync("*", ((channel , value) => {
                Console.WriteLine($"The value which was pushed to REDIS is {value}");
            }));
        }
    }
}