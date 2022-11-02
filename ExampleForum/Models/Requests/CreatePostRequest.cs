namespace ExampleForum.Models.Requests
{
    /// <summary>
    /// Förfrågan att skapa ett nytt inlägg i en tråd.
    /// </summary>
    public class CreatePostRequest
    {
        public string Content { get; set; }

    }
}
