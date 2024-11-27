using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Represents a news article with a title, content, and associated metadata.
    /// </summary>
	public class News
	{
        /// <summary>
        /// Gets or sets the unique identifier for the news article.
        /// </summary>
		public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the news article.
        /// </summary>
		public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author of the news article.
        /// </summary>
		public string Author { get; set; }

        /// <summary>
        /// Gets or sets the content of the news article.
        /// </summary>
		public string Content { get; set; }

        /// <summary>
        /// Gets or sets the category of the news article.
        /// </summary>
		public string Category { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the news article was created.
        /// </summary>
		public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the news article was last updated.
        /// </summary>
		public DateTime? UpdatedAt { get; set; }

        public string? ImageUrl { get; set; }
	}
}
