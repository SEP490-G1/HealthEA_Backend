using Domain.Common.Exceptions;
using Domain.Enum;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.MediatR.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<Guid>
    {
        public Guid EventId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? EventDateTime { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Location { get; set; }
        public EventStatusConstants? Status { get; set; }
        public TimeSpan? ReminderOffset { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
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
            var eventEntity = await _context.Events
                .Include(e => e.UserEvents)
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

            if (eventEntity == null)
                throw new Exception(ErrorCode.EVENT_NOT_FOUND);
                
            eventEntity.Title = request.Title;
            eventEntity.Description = request.Description;
            eventEntity.EventDateTime = request.EventDateTime?.Date ?? eventEntity.EventDateTime;
            eventEntity.StartTime = request.StartTime ?? eventEntity.StartTime;
            eventEntity.EndTime = request.EndTime ?? eventEntity.EndTime;
            eventEntity.Location = request.Location;
            eventEntity.Status = request.Status ?? eventEntity.Status;
            //eventEntity.ReminderOffset = request.ReminderOffset;
            eventEntity.UpdatedAt = request.UpdatedAt;
            eventEntity.UpdatedBy = request.UpdatedBy;

            if (request.UserIds.Any())
            {
                _context.UserEvents.RemoveRange(eventEntity.UserEvents);

                var newEventUsers = new List<UserEvent>();
                foreach (var userId in request.UserIds)
                {
                    var userExists = await _context.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
                    if (!userExists)
                        throw new Exception(ErrorCode.USER_NOT_FOUND);

                    newEventUsers.Add(new UserEvent
                    {
                        UserEventId = Guid.NewGuid(),
                        EventId = eventEntity.EventId,
                        UserId = userId
                    });
                }
                await _context.UserEvents.AddRangeAsync(newEventUsers, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return request.EventId;
        }
    }
}
