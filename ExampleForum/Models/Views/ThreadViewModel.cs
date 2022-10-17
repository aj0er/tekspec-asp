namespace ExampleForum.Models.Views
{
    public class ThreadViewModel
    {

        public Thread Thread { get; set; }
        public IEnumerable<Post> Posts { get; set; }

    }
}
