using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Represents a document profile associated with a patient or user, containing medical content, images, and metadata.
    /// </summary>
    public class DocumentProfile
    {
        /// <summary>
        /// Gets or sets the unique identifier for the document profile.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the document.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the patient related to the document.
        /// </summary>
        public Guid PantientId { get; set; }

        /// <summary>
        /// Gets or sets the type of document, represented as an integer.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the medical content associated with the document.
        /// </summary>
        public string? ContentMedical { get; set; }

        /// <summary>
        /// Gets or sets a list of image URLs or paths associated with the document.
        /// </summary>
        public List<string>? Image { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the document was created.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the document was last modified.
        /// </summary>
        public DateTime LastModifyDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the document, represented as an integer.
        /// </summary>
        public int Status { get; set; }
        //public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the health profile associated with the document.
        /// </summary>
        public virtual HealthProfile? HealthProfile { get; set; }
    }
}
