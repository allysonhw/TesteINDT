using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropostasService.Application.DTOs;
using PropostasService.Application.Services;
using PropostasService.Data;
using PropostasService.Domain.Entities;

namespace PropostasService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropostasController : ControllerBase
{
    private readonly PropostasDbContext _context;
    private readonly PropostaService _propostaService;

    public PropostasController(PropostasDbContext context)
    {
        _context = context;
        _propostaService = new PropostaService();
    }

    [HttpPost]
    public async Task<ActionResult<PropostaDto>> CriarProposta([FromBody] CriarPropostaDto dto)
    {
        var proposta = new Proposta
        {
            Id = Guid.NewGuid(),
            Cpf = dto.Cpf,
            Nome = dto.Nome,
            Idade = dto.Idade,
            Renda = dto.Renda,
            Score = dto.Score,
            ValorSolicitado = dto.ValorSolicitado
        };

        // Analisar proposta
        proposta = _propostaService.AnalisarProposta(proposta);

        // Salvar no banco
        _context.Propostas.Add(proposta);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterProposta), new { id = proposta.Id }, MapToDto(proposta));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropostaDto>> ObterProposta(Guid id)
    {
        var proposta = await _context.Propostas.FindAsync(id);

        if (proposta == null)
            return NotFound();

        return MapToDto(proposta);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult<List<PropostaDto>>> ObterPropostasPorCpf(string cpf)
    {
        var propostas = await _context.Propostas
            .Where(p => p.Cpf == cpf)
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();

        return propostas.Select(MapToDto).ToList();
    }

    [HttpGet]
    public async Task<ActionResult<List<PropostaDto>>> ListarPropostas()
    {
        var propostas = await _context.Propostas
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();

        return propostas.Select(MapToDto).ToList();
    }

    private static PropostaDto MapToDto(Proposta proposta)
    {
        return new PropostaDto
        {
            Id = proposta.Id,
            Cpf = proposta.Cpf,
            Nome = proposta.Nome,
            Idade = proposta.Idade,
            Renda = proposta.Renda,
            Score = proposta.Score,
            ValorSolicitado = proposta.ValorSolicitado,
            Status = proposta.Status.ToString(),
            TaxaJuros = proposta.TaxaJuros,
            MotivoReprovacao = proposta.MotivoReprovacao,
            DataCriacao = proposta.DataCriacao
        };
    }
}
