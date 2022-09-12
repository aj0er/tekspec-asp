using Microsoft.EntityFrameworkCore;
using ExampleForum.Models;
using Thread = ExampleForum.Models.Thread;

namespace ExampleForum.Data
{
    public class ExampleForumContext : DbContext
    {
        public ExampleForumContext (DbContextOptions<ExampleForumContext> options)
            : base(options)
        {
        }

        public DbSet<ExampleForum.Models.User> User { get; set; } = default!;
        public DbSet<Board> Board { get; set; } = default!;
        public DbSet<Post> Post { get; set; } = default!;
        public DbSet<Thread> Thread { get; set; } = default!;


    }
}
