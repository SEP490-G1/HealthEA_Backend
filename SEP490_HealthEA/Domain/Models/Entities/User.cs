

namespace Domain.Models.Entities
{
    /// <summary>
    /// Represents a user entity with relevant information.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateOnly? Dob { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets the phone of the user.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>
        /// Gets or sets the associated doctor for this entity.
        /// </summary>
        /// <remarks>
        /// This property represents a relationship to a <see cref="Doctor"/> entity.
        /// It may be <c>null</c> if no doctor is assigned.
        /// </remarks>
        public virtual Doctor? Doctor { get; set; }

        /// <summary>
        /// Gets or sets the collection of health profiles associated with this entity.
        /// </summary>
        /// <remarks>
        /// This property may be <c>null</c> or empty if no health profiles are assigned.
        /// </remarks>
        public virtual ICollection<HealthProfile>? healthProfiles { get; set; } = new List<HealthProfile>();

        /// <summary>
        /// Gets or sets the collection of user events associated with this entity.
        /// </summary>
        /// <remarks>
        /// This property contains a list of events linked to the user.
        /// </remarks>
        public virtual ICollection<UserEvent> UserEvents { get; set; }

        /// <summary>
        /// Gets or sets the collection of daily metrics recorded for this entity.
        /// </summary>
        /// <remarks>
        /// This property represents daily measurements or statistics.
        /// It may be <c>null</c> or empty if no metrics are available.
        /// </remarks>
        public virtual ICollection<DailyMetric>? DailyMetrics { get; set; } = new List<DailyMetric>();

        /// <summary>
        /// Gets or sets the collection of appointments linked to this entity.
        /// </summary>
        /// <remarks>
        /// This property contains scheduled appointments.
        /// </remarks>
        public virtual ICollection<Appointment> Appointments { get; set; }
    }

}

