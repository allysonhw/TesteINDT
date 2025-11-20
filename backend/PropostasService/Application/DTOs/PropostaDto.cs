namespace PropostasService.Application.DTOs;

public class CriarPropostaDto
{
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal Renda { get; set; }
    public int Score { get; set; }
    public decimal ValorSolicitado { get; set; }
}

public class PropostaDto
{
    public Guid Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public decimal Renda { get; set; }
    public int Score { get; set; }
    public decimal ValorSolicitado { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal? TaxaJuros { get; set; }
    public string? MotivoReprovacao { get; set; }
    public DateTime DataCriacao { get; set; }
}
