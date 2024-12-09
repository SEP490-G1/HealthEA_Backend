using Domain.Common.Exceptions;
using Infrastructure.MediatR.Common;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Appoinment.Queries;

public class GetAppointmentByCustomer : IRequest<PaginatedList<AppointmentDto>>
{
    public Guid UserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetAppointmentByCustomertHandler : IRequestHandler<GetAppointmentByCustomer, PaginatedList<AppointmentDto>>
{
    private readonly SqlDBContext _context;

    public GetAppointmentByCustomertHandler(SqlDBContext context)
    {
        _context = context;
    }
    public async Task<PaginatedList<AppointmentDto>> Handle(GetAppointmentByCustomer request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var customer = await _context.Users.FirstOrDefaultAsync(d => d.UserId == request.UserId);
        if (customer == null)
        {
            throw new Exception(ErrorCode.CUSTOMER_NOT_FOUND);
        }
        var query = _context.Appointments.Where(a => a.UserId == request.UserId)
                              .Select(a => new AppointmentDto
                              {
                                  DoctorId = a.DoctorId,
                                  CustomerId = a.UserId,
                                  CalleeName = customer.FirstName + " " + customer.LastName,
                                  AppointmentId = a.AppointmentId,
                                  Title = a.Title,
                                  Date = a.Date,
                                  Description = a.Description,
                                  StartTime = a.StartTime,
                                  EndTime = a.EndTime,
                                  Location = a.Location,
                                  Status = a.Status,
                                  Uri = a.Uri
                              });

        var result = await PaginatedList<AppointmentDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        return result;
    }
}