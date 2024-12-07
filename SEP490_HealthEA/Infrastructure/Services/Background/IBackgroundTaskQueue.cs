using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Background
{
	public interface IBackgroundTaskQueue
	{
		Task<Func<IServiceProvider, CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
		void QueueBackgroundWorkItem(Func<IServiceProvider, CancellationToken, Task> workItem);
	}
}
