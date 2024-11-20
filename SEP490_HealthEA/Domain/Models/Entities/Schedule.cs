using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

/// <summary>
/// Represents a schedule for a doctor, including date, time, and status.
/// </summary>
public class Schedule
{
    /// <summary>
    /// Gets or sets the unique identifier for the schedule.
    /// </summary>
    public Guid ScheduleId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the doctor associated with this schedule.
    /// </summary>
    public Guid DoctorId { get; set; }

    /// <summary>
    /// Gets or sets the date of the schedule.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the starting time of the schedule.
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Gets or sets the ending time of the schedule.
    /// </summary>
    public TimeSpan EndTime { get; set; }

    /// <summary>
    /// Gets or sets the status of the schedule.
    /// Default value is "Available".
    /// </summary>
    public string Status { get; set; } = "Available";

    /// <summary>
    /// Gets or sets the doctor associated with this schedule.
    /// </summary>
    public virtual Doctor Doctor { get; set; }
}
