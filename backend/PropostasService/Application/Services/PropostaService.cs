using PropostasService.Domain.Entities;

namespace PropostasService.Application.Services;

public class PropostaService
{
    public Proposta AnalisarProposta(Proposta proposta)
    {
        // Validar renda mínima
        if (proposta.Renda < 2000)
        {
            proposta.Reprovar("Renda inferior ao mínimo exigido (R$ 2.000,00)");
            return proposta;
        }

        // Validar score mínimo
        if (proposta.Score < 300)
        {
            proposta.Reprovar("Score de crédito inferior ao mínimo exigido (300)");
            return proposta;
        }

        // Validar valor solicitado vs renda (máximo 10x a renda)
        if (proposta.ValorSolicitado > proposta.Renda * 10)
        {
            proposta.Reprovar("Valor solicitado excede o limite (10x a renda)");
            return proposta;
        }

        // Calcular taxa de juros
        var taxaJuros = CalcularTaxaJuros(proposta.Idade, proposta.Score);
        proposta.Aprovar(taxaJuros);

        return proposta;
    }

    private decimal CalcularTaxaJuros(int idade, int score)
    {
        // Taxa base: 5%
        decimal taxa = 5.0m;

        // Ajuste por idade
        if (idade < 25)
            taxa += 3.0m;
        else if (idade >= 25 && idade <= 40)
            taxa += 1.0m;
        else if (idade > 60)
            taxa += 2.0m;

        // Ajuste por score
        if (score >= 800)
            taxa -= 1.5m;
        else if (score >= 600)
            taxa -= 0.5m;
        else if (score >= 400)
            taxa += 0.5m;
        else
            taxa += 2.0m;

        // Taxa mínima de 2%
        return Math.Max(taxa, 2.0m);
    }
}
