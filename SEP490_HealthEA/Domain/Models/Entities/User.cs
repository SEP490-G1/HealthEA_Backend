

namespace Domain.Models.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public DateOnly? Dob { get; set; }

        public string Email { get; set; } = null!;

        public string? FirstName { get; set; }

        public bool? Gender { get; set; }

        public string? LastName { get; set; }

        public string Password { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Role { get; set; }

        public string? Status { get; set; }

        public string Username { get; set; } = null!;

        public virtual Doctor? Doctor { get; set; }
        public virtual ICollection<HealthProfile>? healthProfiles { get; set; } = new List<HealthProfile>();

        public virtual ICollection<UserEvent> UserEvents { get; set; }

		    public virtual ICollection<DailyMetric>? DailyMetrics { get; set; } = new List<DailyMetric>();
	}

}

