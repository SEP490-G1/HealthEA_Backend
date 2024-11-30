using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MediatR.Appoinment.Queries;

public class AppointmentDto
{
    public Guid AppointmentId { get; set; }
    public Guid CustomerId { get; set; }
    public string? CalleeName { get; set; }
    public Guid DoctorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date {  get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
}
