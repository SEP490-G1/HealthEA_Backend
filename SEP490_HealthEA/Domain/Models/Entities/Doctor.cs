using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
	/// <summary>
	/// Represents a doctor with details about their profile, clinic, and work history.
	/// </summary>
	public class Doctor
	{
		/// <summary>
		/// Gets or sets the unique identifier for the doctor.
		/// </summary>
		public Guid Id { get; set; }

		 /// <summary>
		/// Gets or sets the unique identifier for the user associated with the doctor.
		/// </summary>
		public Guid UserId { get; set; }

		 /// <summary>
		/// Gets or sets the user account associated with the doctor.
		/// </summary>
		/// <remarks>
		/// This property represents a navigation property linking the doctor to a user entity.
		/// It may be <c>null</c> if no user account is associated.
		/// </remarks>

		public User? User { get; set; }
		/// <summary>
		/// Gets or sets the display name of the doctor.
		/// </summary>
        public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets a brief description about the doctor.
		/// </summary>
		/// <remarks>
		/// This property may contain information such as the doctor's expertise, certifications, or other details.
		/// It is optional and can be <c>null</c>.
		/// </remarks>
		public string? Description { get; set; }

		/// <summary>
		/// Gets or sets the address of the doctor's clinic.
		/// </summary>
		public string? ClinicAddress { get; set; }

		/// <summary>
		/// Gets or sets the city where the doctor's clinic is located.
		/// </summary>
		public string? ClinicCity { get; set; }

		/// <summary>
		/// Gets or sets the doctor's specialization field.
		/// </summary>
		/// <remarks>
		/// This property indicates the area of expertise.
		/// </remarks>
		public string? Specialization { get; set; }

		/// <summary>
		/// Gets or sets the number of appointments the doctor has handled.
		/// </summary>
		/// <remarks>
		/// Defaults to <c>0</c> if no appointments have been recorded.
		/// </remarks>
		public int NumberOfAppointments { get; set; } = 0;

		/// <summary>
		/// Gets or sets the number of video calls the doctor has conducted.
		/// </summary>
		/// <remarks>
		/// Defaults to <c>0</c> if no video calls have been recorded.
		/// </remarks>
		public int NumberOfVideoCalls { get; set; } = 0;

		 /// <summary>
		/// Gets or sets the history of the doctor's work in JSON format.
		/// </summary>
		/// <remarks>
		/// This property contains details about the doctor's past experience, roles, and achievements.
		/// Ensure the JSON is well-formed and adheres to the expected schema.
		/// </remarks>
		public string? HistoryOfWork { get; set; } //json
	}
}
