namespace ExampleForum.Models.Response
{
    /// <summary>
    /// Utvald data som ska returneras till API:er för ett inlägg.
    /// <see cref="Post"/>
    /// </summary>
    public class PostResponse
    {

        /// <summary>
        /// Inläggets ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Inläggets innehåll
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Responsen för inläggets skribent
        /// </summary>
        public UserResponse User { get; set; }

    }
}
