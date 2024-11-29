//using Domain.Enum;
//using Infrastructure.SQLServer;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.MediatR.Events.Queries
//{
//    public class GetEventWithUpcoming : IRequest<List<EventDto>>
//    {
//    }

//    public class GetEventWithUpcomingHandler : IRequestHandler<GetEventWithUpcoming, List<EventDto>>
//    {
//        private readonly SqlDBContext _context;

//        public GetEventWithUpcomingHandler(SqlDBContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<EventDto>> Handle(GetEventWithUpcoming request, CancellationToken cancellationToken)
//        {
//            var now = DateTime.UtcNow;
//            var upcomingEvents = await _context.Events
//                .Where(e => e.EventDateTime > now && e.EventDateTime <= now.AddDays(1) && e.Status == EventStatusConstants.Upcoming)
//                .Select(e => new EventDto
//                {
//                    EventId = e.EventId,
//                    Title = e.Title,
//                    Description = e.Description,
//                    EventDateTime = e.EventDateTime,
//                    StartTime = e.StartTime,
//                    EndTime = e.EndTime,
//                    Location = e.Location,
//                    //Status = e.Status,
//                    RepeatFrequency = e.RepeatFrequency,
//                    RepeatInterval = e.RepeatInterval,
//                    RepeatEndDate = e.RepeatEndDate,
//                    UserEvents = e.UserEvents.Select(ue => new UserEventDto
//                    {
//                        UserEventId = ue.UserEventId,
//                        UserId = ue.UserId,
//                        IsAccepted = ue.IsAccepted,
//                        IsOrganizer = ue.IsOrganizer
//                    }).ToList()
//                })
//                .ToListAsync(cancellationToken);

//            return upcomingEvents;
//        }
//    }
//}
