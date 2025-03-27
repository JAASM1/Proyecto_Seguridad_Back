using back_sistema_de_eventos.Models.App;
using Microsoft.EntityFrameworkCore;

namespace back_sistema_de_eventos.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        //User
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invitation>()
           .HasOne(i => i.Event)
           .WithMany(e => e.Invitations)
           .HasForeignKey(i => i.IdEvent)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.IdUser)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
