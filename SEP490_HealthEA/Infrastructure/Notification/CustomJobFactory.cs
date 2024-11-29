using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

namespace Infrastructure.Notification;

public class CustomJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CustomJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        // Lấy dịch vụ từ DI container
        return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        // Quartz sẽ xử lý việc giải phóng job
    }
}
