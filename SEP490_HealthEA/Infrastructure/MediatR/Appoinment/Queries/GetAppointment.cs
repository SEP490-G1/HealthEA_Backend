using Infrastructure.MediatR.Common;
using Infrastructure.SQLServer;
using MediatR;
using System;

namespace Infrastructure.MediatR.Appoinment.Queries;

public class GetAppointment : IRequest<PaginatedList<AppointmentDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetAppointmentHandler : IRequestHandler<GetAppointment, PaginatedList<AppointmentDto>>
{
    private readonly SqlDBContext _context;

    public GetAppointmentHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<AppointmentDto>> Handle(GetAppointment request, CancellationToken cancellationToken)
    {
        var query = _context.Appointments
                              .Select(a => new AppointmentDto
                              {
                                  AppointmentId = a.AppointmentId,
                                  Title = a.Title,
                                  Description = a.Description,
                                  StartTime = a.StartTime,
                                  EndTime = a.EndTime,
                                  Location = a.Location,
                                  Status = a.Status
                              });

        var result = await PaginatedList<AppointmentDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        return result;
    }
}