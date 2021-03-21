using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RugbyResults.Warmup;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RugbyResults
{
    /// <summary>
    /// Bootstraps the application
    /// </summary>
    public class Bootstrap : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of Bootstrap
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        public Bootstrap(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Starts a task to warm up the service
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IWarmupService service = scope.ServiceProvider.GetService<IWarmupService>();
                await service.Execute();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
