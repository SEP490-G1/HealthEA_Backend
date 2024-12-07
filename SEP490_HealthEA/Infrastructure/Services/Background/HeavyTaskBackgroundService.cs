using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Background
{
	public class HeavyTaskBackgroundService : BackgroundService
	{
		private readonly IBackgroundTaskQueue taskQueue;
		private readonly IServiceProvider serviceProvider;

		public HeavyTaskBackgroundService(IBackgroundTaskQueue taskQueue, IServiceProvider serviceProvider)
		{
			this.taskQueue = taskQueue;
			this.serviceProvider = serviceProvider;
		}

		protected async override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				var workItem = await taskQueue.DequeueAsync(stoppingToken);
				try
				{
					using var scope = serviceProvider.CreateScope();
					var scopedServiceProvider = scope.ServiceProvider;

					// Execute the work item with the scoped service provider
					await workItem(scopedServiceProvider, stoppingToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error occurred executing task: {ex.Message}");
				}
			}
		}
	}
}
