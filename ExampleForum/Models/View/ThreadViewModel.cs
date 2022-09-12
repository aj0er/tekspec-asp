namespace ExampleForum.Models.View
{
    public class ThreadViewModel
    {

        public Thread Thread { get; set; }
        public IEnumerable<Post> Posts { get; set; }

    }
}
