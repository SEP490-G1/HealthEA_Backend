using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;
public class UpdateEventCommand : IRequest<Guid>
{
    public Guid EventId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? EventDateTime { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Location { get; set; }
    public EventDailyConstants? RepeatFrequency { get; set; }
    public int? RepeatInterval { get; set; }
    public DateTime? RepeatEndDate { get; set; }
    public List<ReminderOffsetDto> ReminderOffsets { get; set; } = new List<ReminderOffsetDto>();
}
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Guid>
{
    private readonly SqlDBContext _context;

    public UpdateEventCommandHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Tìm sự kiện cần cập nhật
        var eventEntity = await _context.Events
            .Include(e => e.Reminders)
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
            throw new Exception("Event not found");

        // Cập nhật thông tin sự kiện từ request
        eventEntity.Title = request.Title ?? eventEntity.Title;
        eventEntity.Description = request.Description ?? eventEntity.Description;
        eventEntity.EventDateTime = request.EventDateTime?.Date ?? eventEntity.EventDateTime;
        eventEntity.StartTime = request.StartTime ?? eventEntity.StartTime;
        eventEntity.EndTime = request.EndTime ?? eventEntity.EndTime;
        eventEntity.Location = request.Location ?? eventEntity.Location;
        eventEntity.RepeatFrequency = request.RepeatFrequency ?? eventEntity.RepeatFrequency;
        eventEntity.RepeatInterval = request.RepeatInterval ?? eventEntity.RepeatInterval;
        eventEntity.RepeatEndDate = request.RepeatEndDate ?? eventEntity.RepeatEndDate;

        // Xóa các reminders cũ
        _context.Reminders.RemoveRange(eventEntity.Reminders);

        // Tạo lại reminders mới
        DateTime reminderDateTime = eventEntity.EventDateTime.Date.Add(eventEntity.StartTime);
        int interval = eventEntity.RepeatInterval > 0 ? eventEntity.RepeatInterval : 1;

        // Lặp qua các ngày từ ngày bắt đầu đến ngày kết thúc và thêm reminders
        while (reminderDateTime <= eventEntity.RepeatEndDate)
        {
            foreach (var reminderOffsetDto in request.ReminderOffsets)
            {
                var reminderTime = CalculateReminderTime(reminderDateTime, reminderOffsetDto);

                // Thêm reminder vào context
                var reminder = new Reminder
                {
                    ReminderId = Guid.NewGuid(),
                    EventId = eventEntity.EventId,
                    ReminderOffset = reminderOffsetDto.OffsetValue,
                    OffsetUnit = reminderOffsetDto.OffsetUnit,
                    ReminderTime = reminderTime,
                    Message = $"Reminder for event: {eventEntity.Title}",
                    IsSent = false
                };

                await _context.Reminders.AddAsync(reminder, cancellationToken);
            }

            // Cập nhật ngày nhắc nhở tiếp theo dựa trên RepeatFrequency
            switch (eventEntity.RepeatFrequency)
            {
                case EventDailyConstants.Daily:
                    reminderDateTime = reminderDateTime.AddDays(interval);
                    break;

                case EventDailyConstants.Weekly:
                    reminderDateTime = reminderDateTime.AddDays(7 * interval);
                    break;

                case EventDailyConstants.Monthly:
                    reminderDateTime = reminderDateTime.AddMonths(interval);
                    break;

                case EventDailyConstants.Yearly:
                    reminderDateTime = reminderDateTime.AddYears(interval);
                    break;
                case EventDailyConstants.NotRepeat:
                    reminderDateTime = eventEntity.RepeatEndDate.AddDays(1); // Để thoát vòng lặp
                    break;
                default:
                    break;
            }
        }

        // Lưu tất cả thay đổi vào cơ sở dữ liệu
        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }

    private DateTime CalculateReminderTime(DateTime eventDateTime, ReminderOffsetDto reminderOffsetDto)
    {
        return reminderOffsetDto.OffsetUnit switch
        {
            OffsetUnitContants.minutes => eventDateTime.AddMinutes(-reminderOffsetDto.OffsetValue),
            OffsetUnitContants.hours => eventDateTime.AddHours(-reminderOffsetDto.OffsetValue),
            OffsetUnitContants.days => eventDateTime.AddDays(-reminderOffsetDto.OffsetValue),
            OffsetUnitContants.weeks => eventDateTime.AddDays(-reminderOffsetDto.OffsetValue * 7),
            _ => eventDateTime
        };
    }
}
