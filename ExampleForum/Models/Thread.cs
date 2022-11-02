using ExampleForum.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace ExampleForum.Models
{
    /// <summary>
    /// En tråd i systemet. Skapas av en användare och används för att diskutera ämnet som trådskaparen väljer i titeln.
    /// </summary>
    public class Thread
    {

        /// <summary>
        /// Trådens ID
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Trådens namn
        /// </summary>
        public string Name { get; set; }    

        /// <summary>
        /// ID för tavlan som tråden ligger i
        /// </summary>
        public Guid BoardId { get; set; }
        /// <summary>
        /// Tavlan som tråden ligger i
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// ID för användaren som skapade tråden
        /// </summary>
        public string AuthorId { get; set; }
        /// <summary>
        /// Användaren som skapade tråden
        /// </summary>
        public ExampleForumUser Author { get; set; }

    }
}
