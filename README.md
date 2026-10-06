# Desafio C# — 3 exercícios

Três aplicações de console em **C# / .NET 10**, cada uma em seu próprio projeto.

## Estrutura

```
Desafios/
├── Exercicio1/   # Comissão de vendedores
│   ├── Models/Venda.cs
│   ├── Comissao.cs
│   ├── Program.cs
│   └── vendas.json
├── Exercicio2/   # Movimentação de estoque
│   ├── Models/ (DadosEstoque, Movimentacao, Produto, TipoMovimentacao)
│   ├── Services/Deposito.cs
│   ├── Program.cs
│   └── estoque.json
└── Exercicio3/   # Juros por atraso
    ├── Juros.cs
    └── Program.cs
```

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Como executar

Entre na pasta do exercício desejado e rode:

```bash
cd Exercicio1   # ou Exercicio2 / Exercicio3
dotnet run
```

---

## Exercício 1 — Comissão por vendedor

Lê `vendas.json` e calcula a comissão de cada venda, somando por vendedor.

| Valor da venda | Comissão |
|----------------|----------|
| Abaixo de R$ 100,00 | Sem comissão |
| De R$ 100,00 até abaixo de R$ 500,00 | 1% |
| A partir de R$ 500,00 | 5% |

Uma venda de exatamente R$ 500,00 entra na faixa de 5%.

Resultado esperado com o JSON do enunciado:

| Vendedor | Comissão |
|----------|----------|
| João Silva | R$ 495,68 |
| Maria Souza | R$ 465,95 |
| Carlos Oliveira | R$ 379,37 |
| Ana Lima | R$ 404,98 |

## Exercício 2 — Movimentação de estoque

Menu interativo para lançar entradas e saídas dos produtos de `estoque.json`:

```
1 - Entrada de mercadoria
2 - Saída de mercadoria
3 - Consultar estoque
4 - Histórico de movimentações
0 - Sair
```

Cada movimentação possui:

- **Número identificador único** (sequencial, gerado pelo `Deposito`);
- **Descrição** do tipo da movimentação (ex.: Compra, Venda, Devolução, Ajuste), obrigatória;
- Data/hora, produto, tipo, quantidade e saldo final.

Ao final de cada lançamento o programa exibe a **quantidade final em estoque** do produto.

Validações: produto inexistente, quantidade menor ou igual a zero, descrição vazia e saída maior que o saldo disponível.

> O estoque é mantido em memória; ao encerrar o programa, os saldos voltam aos valores do JSON.

## Exercício 3 — Juros por atraso

Calcula os juros na data de hoje a partir de um valor e de uma data de vencimento, com taxa de **2,5% ao dia** (juros simples):

```
juros = valor × 0,025 × dias de atraso
```

Se a data de vencimento for hoje ou futura, os juros são zero.

Os dados podem ser informados interativamente ou por argumentos:

```bash
dotnet run
dotnet run -- 1500,00 01/09/2026
```

Exemplo de saída:

```
Valor original : R$ 1.500,00
Vencimento     : 01/09/2026
Hoje           : 05/10/2026
Dias em atraso : 34
Juros (2,5%/dia): R$ 1.275,00
Total a pagar  : R$ 2.775,00
```

## Decisões de implementação

- Valores monetários usam `decimal`, evitando erros de arredondamento de `double`.
- O enunciado do exercício 3 fala em "multa" e "juros" de 2,5% ao dia; foi adotado **juros simples**. Para juros compostos, basta alterar a fórmula em `Juros.cs`.
- Leitura dos JSONs com `System.Text.Json`, sem dependências externas.
