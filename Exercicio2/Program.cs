using System.Globalization;
using System.Text.Json;
using Exercicio2.Models;
using Exercicio2.Services;

var pt = new CultureInfo("pt-BR");

string caminho = Path.Combine(AppContext.BaseDirectory, "estoque.json");
var dados = JsonSerializer.Deserialize<DadosEstoque>(
    File.ReadAllText(caminho), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
    ?? throw new InvalidOperationException("Não foi possível ler o arquivo de estoque.");

var deposito = new Deposito(dados.Estoque);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Controle de Estoque ===");
    Console.WriteLine("1 - Entrada de mercadoria");
    Console.WriteLine("2 - Saída de mercadoria");
    Console.WriteLine("3 - Consultar estoque");
    Console.WriteLine("4 - Histórico de movimentações");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");

    switch (Console.ReadLine()?.Trim())
    {
        case "1": Lancar(TipoMovimentacao.Entrada); break;
        case "2": Lancar(TipoMovimentacao.Saida); break;
        case "3": ListarEstoque(); break;
        case "4": ListarHistorico(); break;
        case "0": return;
        case null: return; // fim da entrada (stdin fechado)
        default: Console.WriteLine("Opção inválida."); break;
    }
}

void Lancar(TipoMovimentacao tipo)
{
    ListarEstoque();

    Console.Write("Código do produto: ");
    if (!int.TryParse(Console.ReadLine(), out int codigo))
    {
        Console.WriteLine("Código inválido.");
        return;
    }

    Console.Write("Quantidade: ");
    if (!int.TryParse(Console.ReadLine(), out int quantidade))
    {
        Console.WriteLine("Quantidade inválida.");
        return;
    }

    Console.Write("Descrição da movimentação (ex.: Compra, Venda, Devolução, Ajuste): ");
    string descricao = Console.ReadLine()?.Trim() ?? "";

    try
    {
        var mov = deposito.Movimentar(codigo, tipo, quantidade, descricao);
        var produto = deposito.ObterProduto(codigo);

        Console.WriteLine();
        Console.WriteLine($"Movimentação nº {mov.Id} registrada: {mov.Tipo} - {mov.Descricao}");
        Console.WriteLine($"Produto: {produto.DescricaoProduto} (cód. {produto.CodigoProduto})");
        Console.WriteLine($"Quantidade final em estoque: {mov.SaldoFinal}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void ListarEstoque()
{
    Console.WriteLine();
    Console.WriteLine($"{"Cód.",-6}{"Produto",-30}{"Estoque",8}");
    foreach (var p in deposito.Produtos)
        Console.WriteLine($"{p.CodigoProduto,-6}{p.DescricaoProduto,-30}{p.Estoque,8}");
}

void ListarHistorico()
{
    Console.WriteLine();
    if (deposito.Historico.Count == 0)
    {
        Console.WriteLine("Nenhuma movimentação lançada ainda.");
        return;
    }
    Console.WriteLine($"{"Nº",-4}{"Data/hora",-18}{"Cód.",-6}{"Tipo",-9}{"Qtde",6}{"Saldo",8}  Descrição");
    foreach (var m in deposito.Historico)
        Console.WriteLine(
            $"{m.Id,-4}{m.DataHora.ToString("dd/MM/yy HH:mm", pt),-18}{m.CodigoProduto,-6}{m.Tipo,-9}{m.Quantidade,6}{m.SaldoFinal,8}  {m.Descricao}");
}