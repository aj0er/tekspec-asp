namespace ExampleForum.Models.Requests
{
    public class CreatePostRequest
    {
        public Guid ThreadId { get; set; }

        public string Content { get; set; }

    }
}
