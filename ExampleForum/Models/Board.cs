using System.ComponentModel.DataAnnotations;

namespace ExampleForum.Models
{
    public class Board
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
