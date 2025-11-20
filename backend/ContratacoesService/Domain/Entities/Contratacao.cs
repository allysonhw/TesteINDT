namespace ContratacoesService.Domain.Entities;

public enum StatusContratacao
{
    Pendente,
    Aprovada,
    Reprovada
}

public class Contratacao
{
    public Guid Id { get; set; }
    public Guid PropostaId { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public decimal ValorEmprestimo { get; set; }
    public decimal TaxaJuros { get; set; }
    public StatusContratacao Status { get; set; } = StatusContratacao.Pendente;
    public string? MotivoReprovacao { get; set; }
    public DateTime DataContratacao { get; set; } = DateTime.UtcNow;

    public void Aprovar()
    {
        Status = StatusContratacao.Aprovada;
        MotivoReprovacao = null;
    }

    public void Reprovar(string motivo)
    {
        Status = StatusContratacao.Reprovada;
        MotivoReprovacao = motivo;
    }
}
