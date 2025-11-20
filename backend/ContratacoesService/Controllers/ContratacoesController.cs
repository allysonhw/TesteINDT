using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContratacoesService.Application.DTOs;
using ContratacoesService.Application.Services;
using ContratacoesService.Data;
using ContratacoesService.Domain.Entities;

namespace ContratacoesService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratacoesController : ControllerBase
{
    private readonly ContratoesDbContext _context;
    private readonly PropostasApiService _propostasApi;
    private readonly RestricaoService _restricaoService;

    public ContratacoesController(
        ContratoesDbContext context,
        PropostasApiService propostasApi)
    {
        _context = context;
        _propostasApi = propostasApi;
        _restricaoService = new RestricaoService();
    }

    [HttpPost]
    public async Task<ActionResult<ContratacaoDto>> CriarContratacao([FromBody] CriarContratacaoDto dto)
    {
        // Buscar proposta na API
        var proposta = await _propostasApi.ObterProposta(dto.PropostaId);
        
        if (proposta == null)
            return NotFound("Proposta não encontrada");

        // Validar se proposta foi aprovada
        if (proposta.Status != "Aprovada")
            return BadRequest("Apenas propostas aprovadas podem ser contratadas");

        // Verificar se já existe contratação para esta proposta
        var contratacaoExistente = await _context.Contratacoes
            .FirstOrDefaultAsync(c => c.PropostaId == proposta.Id);
        
        if (contratacaoExistente != null)
            return BadRequest("Já existe uma contratação para esta proposta");

        // Criar contratação
        var contratacao = new Contratacao
        {
            Id = Guid.NewGuid(),
            PropostaId = proposta.Id,
            Cpf = proposta.Cpf,
            ValorEmprestimo = proposta.ValorSolicitado,
            TaxaJuros = proposta.TaxaJuros ?? 0
        };

        // Verificar restrições
        var temRestricoes = await _restricaoService.VerificarRestricoes(proposta.Cpf);
        
        if (temRestricoes)
        {
            contratacao.Reprovar("CPF com restrições cadastrais (Serasa/SPC)");
            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();
            return Ok(MapToDto(contratacao));
        }

        // Verificar se cliente tem outras contratações ativas
        var temContratacaoAtiva = await _context.Contratacoes
            .AnyAsync(c => c.Cpf == proposta.Cpf && 
                          c.Status == StatusContratacao.Aprovada && 
                          c.Id != contratacao.Id);

        if (temContratacaoAtiva)
        {
            contratacao.Reprovar("Cliente já possui contratação ativa");
            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();
            return Ok(MapToDto(contratacao));
        }

        // Aprovar contratação
        contratacao.Aprovar();

        _context.Contratacoes.Add(contratacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterContratacao), new { id = contratacao.Id }, MapToDto(contratacao));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContratacaoDto>> ObterContratacao(Guid id)
    {
        var contratacao = await _context.Contratacoes.FindAsync(id);

        if (contratacao == null)
            return NotFound();

        return MapToDto(contratacao);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<List<ContratacaoDto>>> ObterContratacoesPorCpf(string cpf)
    {
        var contratacoes = await _context.Contratacoes
            .Where(c => c.Cpf == cpf)
            .OrderByDescending(c => c.DataContratacao)
            .ToListAsync();

        return contratacoes.Select(MapToDto).ToList();
    }

    [HttpGet]
    public async Task<ActionResult<List<ContratacaoDto>>> ListarContratacoes()
    {
        var contratacoes = await _context.Contratacoes
            .OrderByDescending(c => c.DataContratacao)
            .ToListAsync();

        return contratacoes.Select(MapToDto).ToList();
    }

    private static ContratacaoDto MapToDto(Contratacao contratacao)
    {
        return new ContratacaoDto
        {
            Id = contratacao.Id,
            PropostaId = contratacao.PropostaId,
            Cpf = contratacao.Cpf,
            ValorEmprestimo = contratacao.ValorEmprestimo,
            TaxaJuros = contratacao.TaxaJuros,
            Status = contratacao.Status.ToString(),
            MotivoReprovacao = contratacao.MotivoReprovacao,
            DataContratacao = contratacao.DataContratacao
        };
    }
}
