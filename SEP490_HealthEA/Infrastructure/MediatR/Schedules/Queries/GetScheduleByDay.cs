﻿using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Schedules.Queries;

public class GetScheduleByDayQuery : IRequest<List<ScheduleDto>>
{
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
}
public class GetScheduleByDayHandler : IRequestHandler<GetScheduleByDayQuery, List<ScheduleDto>>
{
    private readonly SqlDBContext _context;

    public GetScheduleByDayHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<ScheduleDto>> Handle(GetScheduleByDayQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == request.DoctorId);
        if (doctor == null)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }
        var query = _context.Schedules
               .Where(s => s.Date == request.Date && s.Status == "Available" && s.DoctorId == doctor.Id);

        var schedules = await query
            .OrderBy(s => s.StartTime)
            .Select(s => new ScheduleDto
            {
                StartTime = s.StartTime,
                ScheduleId = s.ScheduleId,
            })
            .ToListAsync(cancellationToken);

        return schedules;
    }
}
