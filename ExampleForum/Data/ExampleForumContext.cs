using Microsoft.EntityFrameworkCore;
using ExampleForum.Models;
using Thread = ExampleForum.Models.Thread;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ExampleForum.Areas.Identity.Data;

namespace ExampleForum.Data
{
    public class ExampleForumContext : IdentityDbContext<ExampleForumUser>
    {
        public ExampleForumContext(DbContextOptions<ExampleForumContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Board { get; set; } = default!;
        public DbSet<Post> Post { get; set; } = default!;
        public DbSet<Thread> Thread { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExampleForumUser>().Ignore(c => c.PhoneNumber)
                                               .Ignore(c => c.PhoneNumberConfirmed);

        }
    }
}
