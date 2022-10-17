namespace ExampleForum.Models.Response
{
    public class PostResponse
    {

        public Guid Id { get; set; }
        public string Content { get; set; }
        public UserResponse User { get; set; }

    }
}
