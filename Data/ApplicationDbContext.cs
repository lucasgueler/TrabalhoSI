using Microsoft.EntityFrameworkCore;
using SistemaMarmoreGranito.Models;

namespace SistemaMarmoreGranito.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Bloco> Blocos { get; set; }
        public DbSet<Chapa> Chapas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para criptografar a senha do usuário
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Senha)
                .IsRequired();

            // Configuração para garantir que o código do bloco seja único
            modelBuilder.Entity<Bloco>()
                .HasIndex(b => b.Codigo)
                .IsUnique();
        }
    }
} 