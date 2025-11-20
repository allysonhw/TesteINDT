using Microsoft.EntityFrameworkCore;
using ContratacoesService.Domain.Entities;

namespace ContratacoesService.Data;

public class ContratoesDbContext : DbContext
{
    public ContratoesDbContext(DbContextOptions<ContratoesDbContext> options) : base(options)
    {
    }

    public DbSet<Contratacao> Contratacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contratacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            entity.Property(e => e.ValorEmprestimo)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.TaxaJuros)
                .HasColumnType("decimal(5,2)");

            entity.Property(e => e.Status)
                .HasConversion<string>();

            entity.HasIndex(e => e.Cpf);
            entity.HasIndex(e => e.PropostaId);
            entity.HasIndex(e => e.Status);
        });
    }
}
