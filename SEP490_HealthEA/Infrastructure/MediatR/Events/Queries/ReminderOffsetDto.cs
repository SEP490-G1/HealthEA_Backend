using Domain.Enum;
namespace Infrastructure.MediatR.Events.Queries;

public class ReminderOffsetDto
{
    public OffsetUnitContants OffsetUnit { get; set; } = OffsetUnitContants.minutes;
    public int OffsetValue { get; set; } = 1;
}
