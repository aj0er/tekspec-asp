namespace ExampleForum.Models.Requests
{
    public class EditPostRequest
    {

        public Guid Id { get; set; }
        public Guid ThreadId { get; set; }
        public string Content { get; set; }

    }
}
