namespace Exercicio1.Models;

public record Venda(string Vendedor, decimal Valor);
public record VendasArquivo(List<Venda> Vendas);
