namespace ExampleForum.Models.Requests
{
    /// <summary>
    /// Förfrågan att ändra ett existerande inlägg
    /// </summary>
    public class EditPostRequest
    {

        /// <summary>
        /// Det nya innehållet
        /// </summary>
        public string Content { get; set; }

    }
}
