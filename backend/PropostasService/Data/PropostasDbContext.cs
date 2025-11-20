using Microsoft.EntityFrameworkCore;
using PropostasService.Domain.Entities;

namespace PropostasService.Data;

public class PropostasDbContext : DbContext
{
    public PropostasDbContext(DbContextOptions<PropostasDbContext> options) : base(options)
    {
    }

    public DbSet<Proposta> Propostas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Proposta>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Renda)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.ValorSolicitado)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.TaxaJuros)
                .HasColumnType("decimal(5,2)");

            entity.Property(e => e.Status)
                .HasConversion<string>();

            entity.HasIndex(e => e.Cpf);
            entity.HasIndex(e => e.Status);
        });
    }
}
