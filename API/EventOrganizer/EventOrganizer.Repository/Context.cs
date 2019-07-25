using EventOrganizer.Model;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.Repository
{
    public class Context : DbContext
    {
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<UserPicture> UserPictures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        public Context(DbContextOptions<Context> contextOptions) : base(contextOptions) { }
    }
}
