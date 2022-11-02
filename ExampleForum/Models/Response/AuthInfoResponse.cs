namespace ExampleForum.Models.Response
{
    /// <summary>
    /// Respons till förfrågan om autentiseringsuppgifter.
    /// </summary>
    public class AuthInfoResponse
    {

        /// <summary>
        /// Användarens ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Användarens användarnamn
        /// </summary>
        public string UserName { get; set; }

    }
}
