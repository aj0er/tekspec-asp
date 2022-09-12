using System.ComponentModel.DataAnnotations;

namespace ExampleForum.Models
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

    }
}
