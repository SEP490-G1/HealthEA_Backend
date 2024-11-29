using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Schedules.Commands.UpdateSchedule;

public class UpdateScheduleCommand : IRequest<bool>
{
    public Guid ScheduleId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
public class UpdateScheduleHandler : IRequestHandler<UpdateScheduleCommand, bool>
{
    private readonly SqlDBContext _context;

    public UpdateScheduleHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        if (schedule == null)
        {
            throw new Exception("Lịch không tồn tại.");
        }

        if (request.Date < DateTime.UtcNow.Date)
        {
            throw new Exception("Không thể cập nhật lịch cho ngày trong quá khứ.");
        }

        if (request.EndTime <= request.StartTime)
        {
            throw new Exception("Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");
        }

        var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == request.DoctorId, cancellationToken);
        if (!doctorExists)
        {
            throw new Exception(ErrorCode.DOCTOR_NOT_FOUND);
        }

        var overlappingSchedules = await _context.Schedules
            .Where(s => s.DoctorId == request.DoctorId
                        && s.ScheduleId != request.ScheduleId
                        && s.Date == request.Date
                        && ((s.StartTime < request.EndTime && s.EndTime > request.StartTime)))
            .ToListAsync(cancellationToken);

        if (overlappingSchedules.Any())
        {
            throw new Exception("Thời gian đã bị trùng với một lịch khác.");
        }

        schedule.DoctorId = request.DoctorId;
        schedule.Date = request.Date;
        schedule.StartTime = request.StartTime;
        schedule.EndTime = request.EndTime;
        
        _context.Schedules.Update(schedule);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}