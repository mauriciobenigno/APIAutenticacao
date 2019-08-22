using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIAutenticacao.Models
{
    public partial class NetCoreAutenticacaoContext : DbContext
    {
        public NetCoreAutenticacaoContext()
        {
        }

        public NetCoreAutenticacaoContext(DbContextOptions<NetCoreAutenticacaoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbConta> DbConta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=(apiautentica.database.windows.net);Database=dbConta;user=usuarioCit;password=Teste123456;");
                optionsBuilder.UseSqlServer("Server=tcp:apiautentica.database.windows.net,1433;Initial Catalog=dbConta;Persist Security Info=False;User ID=admincit;Password=Teste123456;" +
                    "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbConta>(entity =>
            {
                entity.HasKey(e => e.Usuario);

                entity.ToTable("dbConta");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.SaltCript)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
        }
    }
}
