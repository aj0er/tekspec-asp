namespace ExampleForum.Models
{
    public class BoardContext
    {

        public Board Board { get; set; }
        public IEnumerable<Thread> Threads {get; set;}

    }
}
