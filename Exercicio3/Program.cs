using System.Globalization;
using Exercicio3;

var pt = new CultureInfo("pt-BR");

string? entradaValor = args.Length > 0 ? args[0] : Perguntar("Valor (ex.: 1500,00): ");
string? entradaData = args.Length > 1 ? args[1] : Perguntar("Data de vencimento (dd/MM/aaaa): ");

if (!decimal.TryParse(entradaValor, NumberStyles.Number, pt, out decimal valor) || valor <= 0)
{
    Console.WriteLine("Valor inválido.");
    return 1;
}

if (!DateTime.TryParseExact(entradaData, "dd/MM/yyyy", pt, DateTimeStyles.None, out DateTime vencimento))
{
    Console.WriteLine("Data inválida. Use o formato dd/MM/aaaa.");
    return 1;
}

DateTime hoje = DateTime.Today;
int diasAtraso = Math.Max(0, (hoje.Date - vencimento.Date).Days);
decimal juros = Juros.Calcular(valor, vencimento);

Console.WriteLine();
Console.WriteLine($"Valor original : {valor.ToString("C2", pt)}");
Console.WriteLine($"Vencimento     : {vencimento:dd/MM/yyyy}");
Console.WriteLine($"Hoje           : {hoje:dd/MM/yyyy}");
Console.WriteLine($"Dias em atraso : {diasAtraso}");
Console.WriteLine($"Juros (2,5%/dia): {juros.ToString("C2", pt)}");
Console.WriteLine($"Total a pagar  : {(valor + juros).ToString("C2", pt)}");
return 0;

static string? Perguntar(string texto)
{
    Console.Write(texto);
    return Console.ReadLine();
}
