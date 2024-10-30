using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent;

public class UpdateEventCommand : IRequest<Guid>
{
    public Guid EventId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime EventDateTime { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? Location { get; set; }
    public EventStatusConstants Status { get; set; }
    public EventDailyConstants RepeatFrequency { get; set; }
    public int RepeatInterval { get; set; }
    public DateTime RepeatEndDate { get; set; }
    public TimeSpan? ReminderOffset { get; set; }
    public List<Guid> UserIds { get; set; } = new();
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

        // Kiểm tra sự tồn tại của sự kiện
        var eventEntity = await _context.Events
            .Include(e => e.Reminders)  // Tải nhắc nhở liên quan
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new Exception(ErrorCode.EVENT_NOT_FOUND);
        }

        // Cập nhật thuộc tính sự kiện
        eventEntity.Title = request.Title;
        eventEntity.Description = request.Description;
        eventEntity.EventDateTime = request.EventDateTime.Date;
        eventEntity.StartTime = request.StartTime;
        eventEntity.EndTime = request.EndTime;
        eventEntity.Location = request.Location;
        eventEntity.Status = request.Status;
        eventEntity.RepeatFrequency = request.RepeatFrequency;
        eventEntity.RepeatInterval = request.RepeatInterval;
        eventEntity.RepeatEndDate = request.RepeatEndDate;

        // Xóa tất cả nhắc nhở hiện có trước khi tạo lại các nhắc nhở mới
        _context.Reminders.RemoveRange(eventEntity.Reminders);

        // Tạo lại nhắc nhở mới dựa trên tần suất lặp mới
        DateTime reminderDateTime = request.EventDateTime.Date.Add(request.StartTime);
        int interval = request.RepeatInterval > 0 ? request.RepeatInterval : 1;
        var reminderOffset = request.ReminderOffset ?? TimeSpan.FromMinutes(30);
        OffsetUnitContants offsetUnit = DetermineOffsetUnit(reminderOffset);
        int offsetValue = offsetUnit switch
        {
            OffsetUnitContants.minutes => (int)reminderOffset.TotalMinutes,
            OffsetUnitContants.hours => (int)reminderOffset.TotalHours,
            OffsetUnitContants.days => (int)reminderOffset.TotalDays,
            _ => 0
        };

        var reminders = new List<Reminder>();
        while (reminderDateTime <= request.RepeatEndDate)
        {
            var reminderTime = reminderDateTime.Subtract(reminderOffset);

            reminders.Add(new Reminder
            {
                ReminderId = Guid.NewGuid(),
                EventId = eventEntity.EventId,
                ReminderOffset = offsetValue,
                OffsetUnit = offsetUnit,
                ReminderTime = reminderTime,
                Message = $"Reminder for event: {eventEntity.Title}",
                IsSent = false
            });

            switch (request.RepeatFrequency)
            {
                case EventDailyConstants.Daily:
                    reminderDateTime = reminderDateTime.AddDays(interval);
                    break;
                case EventDailyConstants.Weekyly:
                    reminderDateTime = reminderDateTime.AddDays(7 * interval);
                    break;
                case EventDailyConstants.Monthly:
                    reminderDateTime = reminderDateTime.AddMonths(interval);
                    break;
                case EventDailyConstants.Yearly:
                    reminderDateTime = reminderDateTime.AddYears(interval);
                    break;
                case EventDailyConstants.NotRepeat:
                    reminderDateTime = request.RepeatEndDate.AddDays(1); // Thoát khỏi vòng lặp
                    break;
                default:
                    break;
            }
        }

        // Thêm tất cả các nhắc nhở mới vào cơ sở dữ liệu
        await _context.Reminders.AddRangeAsync(reminders, cancellationToken);

        // Cập nhật danh sách người dùng
        var existingUserIds = await _context.UserEvents
            .Where(ue => ue.EventId == request.EventId)
            .Select(ue => ue.UserId)
            .ToListAsync(cancellationToken);

        var usersToAdd = request.UserIds.Except(existingUserIds).ToList();
        var usersToRemove = existingUserIds.Except(request.UserIds).ToList();

        foreach (var userId in usersToRemove)
        {
            var userEvent = await _context.UserEvents.FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == request.EventId, cancellationToken);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
            }
        }

        foreach (var userId in usersToAdd)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
            if (!userExists)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }

            var eventUser = new UserEvent
            {
                UserEventId = Guid.NewGuid(),
                EventId = eventEntity.EventId,
                UserId = userId
            };

            await _context.UserEvents.AddAsync(eventUser, cancellationToken);
        }

        // Lưu tất cả thay đổi vào cơ sở dữ liệu
        await _context.SaveChangesAsync(cancellationToken);

        return eventEntity.EventId;
    }


    // Phương thức xác định OffsetUnit từ ReminderOffset
    private OffsetUnitContants DetermineOffsetUnit(TimeSpan offset)
    {
        if (offset.TotalMinutes < 60)
            return OffsetUnitContants.minutes;
        if (offset.TotalHours < 24)
            return OffsetUnitContants.hours;
        return OffsetUnitContants.days;
    }
}
