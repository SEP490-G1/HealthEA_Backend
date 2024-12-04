//using Microsoft.Extensions.Hosting;
//using Quartz;

//namespace Infrastructure.Notification;

//public class QuartzHostedService : IHostedService
//{
//    private readonly ISchedulerFactory _schedulerFactory;
//    private IScheduler _scheduler;

//    public QuartzHostedService(ISchedulerFactory schedulerFactory)
//    {
//        _schedulerFactory = schedulerFactory;
//    }

//    public async Task StartAsync(CancellationToken cancellationToken)
//    {
//        try
//        {
//            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
//            await _scheduler.Start(cancellationToken);
//        }
//        catch (Exception ex)
//        {

//        }

//    }

//    public async Task StopAsync(CancellationToken cancellationToken)
//    {
//        if (_scheduler != null)
//        {
//            await _scheduler.Shutdown(cancellationToken);
//        }
//    }
//}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System.Globalization;

namespace Infrastructure.Notification
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private IScheduler _scheduler;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IServiceScopeFactory scopeFactory)
        {
            _schedulerFactory = schedulerFactory;
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
                await _scheduler.Start(cancellationToken);

                await CreateTriggers(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting Quartz Scheduler: {ex.Message}");
            }
        }

        private async Task CreateTriggers(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var reminderService = scope.ServiceProvider.GetRequiredService<ReminderService>();

                var reminderTimes = reminderService.GetReminderTimes();

                var jobKey = new JobKey("reminderJob");

                var jobDetail = JobBuilder.Create<ReminderJob>()
                    .WithIdentity(jobKey)
                    .Build();

                foreach (var reminderTime in reminderTimes)
                {
                    string cronExpression = $"0 {reminderTime.Minute} {reminderTime.Hour} * * ?";

                    var trigger = TriggerBuilder.Create()
                        .ForJob(jobDetail)
                        .WithIdentity($"reminderTrigger-{reminderTime:HHmm}")
                        .UsingJobData("reminderTime", reminderTime.ToString("HH:mm", CultureInfo.InvariantCulture))
                        .WithCronSchedule(cronExpression)
                        .Build();

                    await _scheduler.ScheduleJob(trigger, cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_scheduler != null)
            {
                await _scheduler.Shutdown(cancellationToken);
            }
        }
    }
}



