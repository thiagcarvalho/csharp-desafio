namespace Exercicio3;

public static class Juros
{
    // 2,5% ao dia, juros simples sobre os dias de atraso.
    public static decimal Calcular(decimal valor, DateTime vencimento)
    {
        var dias = (DateTime.Today - vencimento.Date).Days;
        return dias <= 0 ? 0 : Math.Round(valor * 0.025m * dias, 2, MidpointRounding.AwayFromZero);
    }
}
