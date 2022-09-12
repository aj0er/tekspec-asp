using System.ComponentModel.DataAnnotations;

namespace ExampleForum.Models
{
    public class Thread
    {

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }    

        public Guid BoardId { get; set; }
        public Board Board { get; set; }

    }
}
