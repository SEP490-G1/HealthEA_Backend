using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Status { get; set; } = "Available";
    public virtual Doctor Doctor { get; set; }
}
