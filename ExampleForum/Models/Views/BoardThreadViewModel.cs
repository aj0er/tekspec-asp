namespace ExampleForum.Models.Views
{
    public class BoardThreadViewModel
    {

        public Board Board { get; set; }
        public IEnumerable<Thread> Threads { get; set; }

    }
}
