using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;
    public virtual Doctor Doctor { get; set; }
}
