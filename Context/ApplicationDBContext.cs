using Microsoft.EntityFrameworkCore;

namespace back_sistema_de_eventos.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
