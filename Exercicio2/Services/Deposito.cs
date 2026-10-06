using Exercicio2.Models;

namespace Exercicio2.Services;

public class Deposito
{
    private readonly Dictionary<int, Produto> _produtos;
    private readonly List<Movimentacao> _historico = new();
    private int _proximoId = 1; // identificador único e sequencial

    public Deposito(IEnumerable<Produto> produtos) =>
        _produtos = produtos.ToDictionary(p => p.CodigoProduto);

    public IEnumerable<Produto> Produtos => _produtos.Values.OrderBy(p => p.CodigoProduto);
    public IReadOnlyList<Movimentacao> Historico => _historico;

    public Produto ObterProduto(int codigo) =>
        _produtos.TryGetValue(codigo, out var p)
            ? p
            : throw new ArgumentException($"Produto {codigo} não encontrado.");

    public Movimentacao Movimentar(int codigo, TipoMovimentacao tipo, int quantidade, string descricao)
    {
        var produto = ObterProduto(codigo);

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição da movimentação é obrigatória.");

        if (tipo == TipoMovimentacao.Saida && quantidade > produto.Estoque)
            throw new InvalidOperationException(
                $"Estoque insuficiente. Disponível: {produto.Estoque}, solicitado: {quantidade}.");

        produto.Estoque += tipo == TipoMovimentacao.Entrada ? quantidade : -quantidade;

        var mov = new Movimentacao
        {
            Id = _proximoId++,
            DataHora = DateTime.Now,
            CodigoProduto = codigo,
            Tipo = tipo,
            Descricao = descricao.Trim(),
            Quantidade = quantidade,
            SaldoFinal = produto.Estoque
        };

        _historico.Add(mov);
        return mov;
    }
}