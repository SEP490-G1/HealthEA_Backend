using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Represents a health profile containing personal and medical information.
    /// </summary>
    public class HealthProfile
    {

        /// <summary>
        /// Gets or sets the unique identifier for the health profile.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the health profile.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique profile code for the health profile.
        /// </summary>
        [Required]
        public required string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets the full name of the individual associated with the health profile.
        /// </summary>
        [Required]
        public required string FullName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the individual.
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the individual.
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Gets or sets the residence information of the individual.
        /// </summary>
        public string? Residence { get; set; }

        /// <summary>
        /// Gets or sets additional notes related to the health profile.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Gets or sets the sharing status of the health profile.
        /// </summary>
        /// <remarks>
        /// The sharing status can have the following values:
        /// <list type="bullet">
        /// <item><description>0 - Private</description></item>
        /// <item><description>1 - Doctor can view</description></item>
        /// <item><description>2 - User can view</description></item>
        /// <item><description>3 - Public</description></item>
        /// </list>
        /// </remarks>
        public int SharedStatus { get; set; }

        /// <summary>
        /// Gets or sets the date when the health profile was created.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the health profile was last modified.
        /// </summary>
        public DateTime LastModifyDate { get; set; }


        /// <summary>
        /// Gets or sets the collection of document profiles associated with this health profile.
        /// </summary>
        public virtual ICollection<DocumentProfile> DocumentProfiles { get; set; } = new List<DocumentProfile>();

        /// <summary>
        /// Gets or sets the user associated with this health profile.
        /// </summary>
        /// <remarks>
        /// This property is a navigation property linking the health profile to a user entity.
        /// It may be <c>null</c> if no user is associated.
        /// </remarks>
        public virtual User? User { get; set; }
    }
}
