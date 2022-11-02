using System.ComponentModel.DataAnnotations;

namespace ExampleForum.Models
{
    /// <summary>
    /// En tavla i systemet. 
    /// Har ett namn och används för att kategorisera trådar av liknande ämne.
    /// </summary>
    public class Board
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
