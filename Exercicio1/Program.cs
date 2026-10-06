using System.Text.Json;
using Exercicio1;
using Exercicio1.Models;

var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var vendas = JsonSerializer.Deserialize<VendasArquivo>(File.ReadAllText("vendas.json"), opt)!.Vendas;

foreach (var (vendedor, total) in Comissao.PorVendedor(vendas))
    Console.WriteLine($"{vendedor}: {total:C}");
