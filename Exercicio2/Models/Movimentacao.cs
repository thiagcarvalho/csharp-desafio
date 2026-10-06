namespace Exercicio2.Models;

public class Movimentacao
{
    public int Id { get; init; }
    public DateTime DataHora { get; init; }
    public int CodigoProduto { get; init; }
    public TipoMovimentacao Tipo { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public int Quantidade { get; init; }
    public int SaldoFinal { get; init; }
}