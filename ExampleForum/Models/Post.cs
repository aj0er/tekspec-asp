using ExampleForum.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExampleForum.Models
{
    public class Post
    {

        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }

        [JsonIgnore]
        public Guid ThreadId { get; set; }
        public Thread Thread { get; set; }

        [JsonIgnore]
        public string AuthorId { get; set; }
        public ExampleForumUser Author { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
