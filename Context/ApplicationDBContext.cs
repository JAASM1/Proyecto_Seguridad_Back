using back_sistema_de_eventos.Models.App;
using Microsoft.EntityFrameworkCore;

namespace back_sistema_de_eventos.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        //User
        public DbSet<User> Users { get; set; }

        //Event
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasOne(e=>e.Organizer)
                .WithMany(u => u.OrganaizedEvents)
                .HasForeignKey(e => e.IdOrganizer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Invitations)
                .HasForeignKey(i => i.IdEvent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<Invitation>()
                .HasIndex(i => new { i.IdEvent, i.IdUser })
                .IsUnique();

        }
    }
}
