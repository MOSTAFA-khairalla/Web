using Core.Enities;
using Microsoft.EntityFrameworkCore;

namespace infrastrcture.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
