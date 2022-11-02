using ExampleForum.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExampleForum.Models
{
    /// <summary>
    /// Ett inlägg i systemet. Ligger i en tråd och skapas av vanliga användare.
    /// </summary>
    public class Post
    {

        /// <summary>
        /// Inläggets ID
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Inläggets innehåll
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// ID för tråden som inlägget ligger i
        /// </summary>
        [JsonIgnore]
        public Guid ThreadId { get; set; }
        /// <summary>
        /// Tråden som inlägget ligger i
        /// </summary>
        public Thread Thread { get; set; }

        /// <summary>
        /// ID för användaren som skapade inlägget
        /// </summary>
        [JsonIgnore]
        public string AuthorId { get; set; }
        /// <summary>
        /// Användaren som skapade inlägget
        /// </summary>
        public ExampleForumUser Author { get; set; }

        /// <summary>
        /// Datum och tid då inlägget skapades
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Datum och tid då inlägget uppdaterades
        /// </summary>
        public DateTime Updated { get; set; }

    }
}
