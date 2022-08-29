using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExampleForum.Models;

namespace ExampleForum.Data
{
    public class ExampleForumContext : DbContext
    {
        public ExampleForumContext (DbContextOptions<ExampleForumContext> options)
            : base(options)
        {
        }

        public DbSet<ExampleForum.Models.User> User { get; set; } = default!;
    }
}
