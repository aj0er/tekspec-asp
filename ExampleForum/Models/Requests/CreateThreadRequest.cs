namespace ExampleForum.Models.Requests
{
    /// <summary>
    /// Förfrågan att skapa en ny tråd i en tavla.
    /// </summary>
    public class CreateThreadRequest
    {

        /// <summary>
        /// Trådens titel
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Innehållet för det första associerade inlägget 
        /// </summary>
        public string Content { get; set; }

    }
}
