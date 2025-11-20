namespace PropostasService.Domain.Entities;

public enum StatusProposta
{
    Pendente,
    Aprovada,
    Reprovada
}

public class Proposta
{
    public Guid Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal Renda { get; set; }
    public int Score { get; set; }
    public decimal ValorSolicitado { get; set; }
    public StatusProposta Status { get; set; } = StatusProposta.Pendente;
    public decimal? TaxaJuros { get; set; }
    public string? MotivoReprovacao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public void Aprovar(decimal taxaJuros)
    {
        Status = StatusProposta.Aprovada;
        TaxaJuros = taxaJuros;
        MotivoReprovacao = null;
    }

    public void Reprovar(string motivo)
    {
        Status = StatusProposta.Reprovada;
        MotivoReprovacao = motivo;
        TaxaJuros = null;
    }
}
