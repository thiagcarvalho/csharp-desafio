using Exercicio1.Models;

namespace Exercicio1;

public static class Comissao
{
    public static decimal Calcular(decimal valor) =>
        valor < 100 ? 0 : valor < 500 ? valor * 0.01m : valor * 0.05m;

    public static Dictionary<string, decimal> PorVendedor(IEnumerable<Venda> vendas) =>
        vendas.GroupBy(v => v.Vendedor)
              .ToDictionary(g => g.Key, g => Math.Round(g.Sum(v => Calcular(v.Valor)), 2));
}
