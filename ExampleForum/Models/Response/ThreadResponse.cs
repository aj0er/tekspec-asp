namespace ExampleForum.Models.Response
{
    /// <summary>
    /// Utvald data som ska returneras till API:er för en tråd.
    /// </summary>
    public class ThreadResponse
    {

        /// <summary>
        /// Trådens ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Trådens namn
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ID för tavlan som tråden ligger i
        /// </summary>
        public Guid Board { get; set; }
        /// <summary>
        /// ID för användaren som skapade tråden
        /// </summary>
        public string Author { get; set; }

    }
}
