using Domain.Common.Exceptions;
using Infrastructure.MediatR.Common;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.MediatR.Appoinment.Queries;

public class GetAppointment : IRequest<PaginatedList<AppointmentDto>>
{
    public Guid UserId { get; set; }
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
        cancellationToken.ThrowIfCancellationRequested();
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == request.UserId);
        var user = await _context.Users.FirstOrDefaultAsync(d => d.UserId == doctor.UserId);
        if (doctor == null)
        {
            if (user == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
        }

        var query = _context.Appointments.Where(a => a.DoctorId == doctor.Id).Include(a => a.Users)
                              .Select(a => new AppointmentDto
                              {
                                  DoctorId = user.UserId,
                                  CustomerId = a.UserId,
                                  CalleeName = user.FirstName + " " + user.LastName,
                                  NameCustomer = a.Users.FirstName + " " + a.Users.LastName,
                                  Uri = a.Uri,
                                  AppointmentId = a.AppointmentId,
                                  Title = a.Title,
                                  Date = a.Date,
                                  Description = a.Description,
                                  StartTime = a.StartTime,
                                  EndTime = a.EndTime,
                                  Location = a.Location,
                                  Status = a.Status,
                                  CreatedAt = a.CreatedAt
                              }).OrderByDescending(x=>x.CreatedAt);

        var result = await PaginatedList<AppointmentDto>.CreateAsync(query, request.PageNumber, request.PageSize);
        return result;
    }
}