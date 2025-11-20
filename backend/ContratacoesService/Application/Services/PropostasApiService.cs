using System.Net.Http.Json;
using ContratacoesService.Application.DTOs;

namespace ContratacoesService.Application.Services;

public class PropostasApiService
{
    private readonly HttpClient _httpClient;

    public PropostasApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PropostaDto?> ObterProposta(Guid propostaId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/propostas/{propostaId}");
            
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<PropostaDto>();
        }
        catch
        {
            return null;
        }
    }
}
