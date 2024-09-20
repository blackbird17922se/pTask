using Microsoft.EntityFrameworkCore;
using pTask.Model;

namespace pTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Agrega un usuario de forma predeterminada
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123") // Usamos BCrypt para encriptar la contraseña
                }
            );
        }

        // representacion de las tablas
        public DbSet<TareaItem> TareaItem {  get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
