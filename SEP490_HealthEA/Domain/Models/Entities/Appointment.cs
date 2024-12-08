using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Appointment
{
    public Guid AppointmentId { get; set; } = Guid.NewGuid();
    public Guid DoctorId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string? Location { get; set; }
    [Column("url")]
    public string? Uri { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Type { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public virtual Doctor Doctors { get; set; }
    public virtual User Users { get; set; }
}
