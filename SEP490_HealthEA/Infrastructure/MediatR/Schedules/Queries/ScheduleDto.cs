namespace Infrastructure.MediatR.Schedules.Queries;

public class ScheduleDto
{
    public Guid ScheduleId { get; set; }

	public TimeSpan StartTime { get; set; }
}
