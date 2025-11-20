using ContratacoesService.Domain.Entities;

namespace ContratacoesService.Application.Services;

public class RestricaoService
{
    // Simula CPFs com restrições no Serasa/SPC
    private static readonly HashSet<string> CpfsComRestricao = new()
    {
        "12345678901",
        "98765432100"
    };

    public Task<bool> VerificarRestricoes(string cpf)
    {
        // Simula verificação em sistemas externos
        return Task.FromResult(CpfsComRestricao.Contains(cpf));
    }
}
