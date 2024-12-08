using Domain.Common.Exceptions;
using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Schedules.Commands.CreateSchedule;

public class CreateScheduleCommand : IRequest<List<Schedule>>
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int SlotDurationInMinutes { get; set; } = 15;
}
public class CreateScheduleHandler : IRequestHandler<CreateScheduleCommand, List<Schedule>>
{
    private readonly SqlDBContext _context;

    public CreateScheduleHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<Schedule>> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedules = new List<Schedule>();
        TimeSpan slotDuration = TimeSpan.FromMinutes(request.SlotDurationInMinutes);
        if (request.Date.Date < DateTime.UtcNow.Date)
        {
            throw new Exception("Không thể tạo lịch cho ngày trong quá khứ.");
        }

        //var doctorExists = await _context.Doctors.AnyAsync(d => d.SelectedProfileId == request.DoctorId, cancellationToken);
        var doctorExists = await _context.Doctors
            .FirstOrDefaultAsync(d => d.UserId == request.UserId, cancellationToken);
        if (doctorExists == null)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }

        var overlappingSchedules = await _context.Schedules
            .Where(s => s.DoctorId == doctorExists.Id
                        && s.Date == request.Date
                        && ((s.StartTime < request.EndTime && s.EndTime > request.StartTime)))
            .ToListAsync(cancellationToken);

        if (overlappingSchedules.Any())
        {
            _context.Schedules.RemoveRange(overlappingSchedules);
            await _context.SaveChangesAsync(cancellationToken);
        }

        for (var time = request.StartTime; time < request.EndTime; time += slotDuration)
        {
            var schedule = new Schedule
            {
                ScheduleId = Guid.NewGuid(),
                DoctorId = doctorExists.Id,
                Date = request.Date,
                StartTime = time,
                EndTime = time + slotDuration,
                Status = "Available",
            };

            schedules.Add(schedule);
            await _context.Schedules.AddAsync(schedule, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return schedules;
    }
}