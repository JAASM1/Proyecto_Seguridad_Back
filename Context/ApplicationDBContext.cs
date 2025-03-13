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
        public DbSet<GuestRegistration> GuestRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                 .HasOne(e => e.Organizer)
                 .WithMany(u => u.OrganizedEvents)
                 .HasForeignKey(e => e.IdOrganizer)
                 .OnDelete(DeleteBehavior.Restrict);

            //índice para el token del evento
            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Token)
                .IsUnique();

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Invitations)
                .HasForeignKey(i => i.IdEvent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Guest)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.IdGuest)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GuestRegistration>()
                .HasOne(g => g.Invitation)
                .WithOne(i => i.GuestRegistration)
                .HasForeignKey<GuestRegistration>(g => g.IdInvitation)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GuestRegistration>()
                .HasOne(g => g.User)
                .WithMany(u => u.GuestRegistrations)
                .HasForeignKey(g => g.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
