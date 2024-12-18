﻿using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Schedules.Commands.DeleteSchedule;

public class DeleteScheduleCommand : IRequest<bool>
{
    public Guid ScheduleId { get; set; }
    public Guid UserId { get; set; }
}
public class DeleteScheduleHandler : IRequestHandler<DeleteScheduleCommand, bool>
{
    private readonly SqlDBContext _context;

    public DeleteScheduleHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d=>d.UserId == request.UserId);
        if (doctor == null)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }

        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId && s.DoctorId == doctor.Id, cancellationToken);

        if (schedule == null)
        {
            throw new Exception(ErrorCode.UNAUTHORIZED_SCHEDULE);
        }

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

