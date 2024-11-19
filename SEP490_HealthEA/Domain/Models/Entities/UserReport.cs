using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Represents a user report submitted by a user regarding another user or issue.
    /// </summary>
	public class UserReport
	{
        /// <summary>
        /// Gets or sets the unique identifier for the report.
        /// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who submitted the report.
        /// </summary>
		public Guid ReporterId { get; set; }

        /// <summary>
        /// Gets or sets the reporter's user details.
        /// </summary>
		public User Reporter { get; set; }

        /// <summary>
        /// Gets or sets the type of report (e.g., spam, abuse, inappropriate content).
        /// </summary>
		public string ReportType { get; set; }

        /// <summary>
        /// Gets or sets a description of the report, providing more details about the issue.
        /// </summary>
		public string ReportDescription { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user who is being reported.
        /// </summary>
		public Guid ReportedId {  get; set; }

        /// <summary>
        /// Gets or sets the status of the report.
        /// </summary>
		public int Status { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the report was created.
        /// </summary>
		public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the report was resolved, if applicable.
        /// </summary>
		public DateTime? ResolvedAt { get; set; }
	}

}
