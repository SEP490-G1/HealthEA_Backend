using Infrastructure.MediatR.Common;
using Infrastructure.SQLServer;
using MediatR;

namespace Infrastructure.MediatR.Appoinment.Queries;

public class GetAppointmentByUserId : IRequest<PaginatedList<AppointmentDto>>
{
    public Guid UserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetAppointmentByUserIdHandler : IRequestHandler<GetAppointmentByUserId, PaginatedList<AppointmentDto>>
{
    private readonly SqlDBContext _context;

    public GetAppointmentByUserIdHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<AppointmentDto>> Handle(GetAppointmentByUserId request, CancellationToken cancellationToken)
    {
        var query = _context.Appointments
                              .Where(a => a.UserId == request.UserId)
                              .Select(a => new AppointmentDto
                              {
                                  AppointmentId = a.AppointmentId,
                                  Title = a.Title,
                                  Description = a.Description,
                                  Date = a.Date,
                                  StartTime = a.StartTime,
                                  EndTime = a.EndTime,
                                  Location = a.Location,
                                  Status = a.Status
                              });

        return await PaginatedList<AppointmentDto>.CreateAsync(query, request.PageNumber, request.PageSize);
    }
}