namespace ContratacoesService.Application.DTOs;

public class CriarContratacaoDto
{
    public Guid PropostaId { get; set; }
}

public class ContratacaoDto
{
    public Guid Id { get; set; }
    public Guid PropostaId { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public decimal ValorEmprestimo { get; set; }
    public decimal TaxaJuros { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? MotivoReprovacao { get; set; }
    public DateTime DataContratacao { get; set; }
}
