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
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
