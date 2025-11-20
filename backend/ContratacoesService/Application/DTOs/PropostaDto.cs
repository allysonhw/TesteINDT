namespace ContratacoesService.Application.DTOs;

public class PropostaDto
{
    public Guid Id { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal ValorSolicitado { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal? TaxaJuros { get; set; }
}
