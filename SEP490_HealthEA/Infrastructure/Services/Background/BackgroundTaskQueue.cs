using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infrastructure.Services.Background
{
	public class BackgroundTaskQueue : IBackgroundTaskQueue
	{
		private readonly Channel<Func<IServiceProvider, CancellationToken, Task>> queue;

		public BackgroundTaskQueue()
		{
			queue = Channel.CreateUnbounded<Func<IServiceProvider, CancellationToken, Task>>();
		}

		public void QueueBackgroundWorkItem(Func<IServiceProvider, CancellationToken, Task> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException(nameof(workItem));
			}
			queue.Writer.TryWrite(workItem);
		}

		public async Task<Func<IServiceProvider, CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
		{
			return await queue.Reader.ReadAsync(cancellationToken);
		}
	}
}
