using Newtonsoft.Json;
using target_sistemas_estoque;

var executando = true;

string estoque = @"{
	""estoque"":
	[
	  {
		""codigoProduto"": 101,
		""descricaoProduto"": ""Caneta Azul"",
		""estoque"": 150
	  },
	  {
		""codigoProduto"": 102,
		""descricaoProduto"": ""Caderno Universitário"",
		""estoque"": 75
	  },
	  {
		""codigoProduto"": 103,
		""descricaoProduto"": ""Borracha Branca"",
		""estoque"": 200
	  },
	  {
		""codigoProduto"": 104,
		""descricaoProduto"": ""Lápis Preto HB"",
		""estoque"": 320
	  },
	  {
		""codigoProduto"": 105,
		""descricaoProduto"": ""Marcador de Texto Amarelo"",
		""estoque"": 90
	  }
	]
}
";

EstoqueJson? produtos = !string.IsNullOrEmpty(estoque) ? JsonConvert.DeserializeObject<EstoqueJson>(estoque) : null;

List<Movimentacoes> movimentacoes = new List<Movimentacoes>();

if (produtos != null && produtos.Estoque.Count > 0)
{
    while (executando)
    {
        Console.WriteLine(".:Sistema de Controle de Estoque:.");
        Console.WriteLine();
        Console.WriteLine("Código - Descrição");
        foreach (var item in produtos.Estoque)
        {
            Console.WriteLine(item.CodigoProduto + " - " + item.DescricaoProduto);
        }

        Console.WriteLine();

        Console.WriteLine("1 - Lançar Movimentação");
        Console.WriteLine("2 - Listar Movimentaçoes");

        var opcaoInput = Console.ReadLine();

        int opcao;

        var parseOpcao = int.TryParse(opcaoInput, out opcao);

        while (!parseOpcao || (opcao != 1 && opcao != 2))
        {
            Console.WriteLine("1 - Lançar Movimentação");
            Console.WriteLine("2 - Listar Movimentaçoes");

            opcaoInput = Console.ReadLine();

            parseOpcao = int.TryParse(opcaoInput, out opcao);
        }

        if (opcao == 1)
        {
            Console.Write("Digite o código do produto que deseja alterar: ");
            var codigo = Console.ReadLine();

            if (!string.IsNullOrEmpty(codigo) && produtos.Estoque.Any(x => x.CodigoProduto == int.Parse(codigo)))
            {
                Console.Clear();
                var produto = produtos.Estoque.First(x => x.CodigoProduto == int.Parse(codigo));

                Console.WriteLine(".:Movimentar Estoque:.");
                Console.WriteLine("Código - Descrição - Quantidade");
                Console.WriteLine(produto.CodigoProduto + " - " + produto.DescricaoProduto + " - " + produto.Estoque);
                Console.WriteLine();
                Console.WriteLine("Selecione o tipo da movimentação");
                Console.WriteLine($"{(int)MovimentacoesEnum.Entrada} - Entrada");
                Console.WriteLine($"{(int)MovimentacoesEnum.Saida} - Saída");

                var movimentacaoInput = Console.ReadLine();

                int movimentacao;

                var parse = int.TryParse(movimentacaoInput, out movimentacao);

                while (!parse || (movimentacao != (int)MovimentacoesEnum.Entrada && movimentacao != (int)MovimentacoesEnum.Saida))
                {
                    Console.WriteLine($"Selecione o tipo da movimentação ( {(int)MovimentacoesEnum.Entrada} ou {(int)MovimentacoesEnum.Saida} )");
                    Console.WriteLine($"{(int)MovimentacoesEnum.Entrada} - Entrada");
                    Console.WriteLine($"{(int)MovimentacoesEnum.Saida} - Saída");

                    movimentacaoInput = Console.ReadLine();

                    parse = int.TryParse(movimentacaoInput, out movimentacao);
                }

                Console.WriteLine();
                Console.WriteLine("Informe a quantidade");

                var quantidadeInput = Console.ReadLine();

                int quantidade;

                parse = int.TryParse(quantidadeInput, out quantidade);

                while (!parse)
                {
                    Console.WriteLine("Informe a quantidade");

                    quantidadeInput = Console.ReadLine();

                    parse = int.TryParse(quantidadeInput, out quantidade);
                }

                Console.Clear();

                var novoEstoque = 
                    movimentacao == (int)MovimentacoesEnum.Entrada ? produto.Estoque + quantidade :
                    movimentacao == (int)MovimentacoesEnum.Saida ? produto.Estoque - quantidade :
                    produto.Estoque;

                produtos.Estoque.First(p => p.CodigoProduto == produto.CodigoProduto).Estoque = novoEstoque;

                movimentacoes.Add(new Movimentacoes
                {
                    Id = Guid.NewGuid(),
                    CodigoProduto = produto.CodigoProduto,
                    Descricao = (MovimentacoesEnum)movimentacao,
                    QuantidadeMovimentada = quantidade,
                });

                Console.WriteLine($"Produto {produto.DescricaoProduto} alterado");
                Console.WriteLine($"Estoque atual: {produto.Estoque}");
                Console.WriteLine();
                Console.WriteLine("Pressione enter para continuar");
                Console.ReadLine();
            }
        }

        else if (opcao == 2 && movimentacoes.Count > 0)
        {
            Console.Clear();
            Console.WriteLine("Id - Descrição - Produto - Quantidade Movimentada");
            foreach (var m in movimentacoes)
            {
                var descricao = m.Descricao == MovimentacoesEnum.Entrada ? "Entrada" : "Saída";
                var produto = produtos.Estoque.First(x => x.CodigoProduto == m.CodigoProduto);
                Console.WriteLine(m.Id + " - " + descricao + " - " + produto.DescricaoProduto + " - " + m.QuantidadeMovimentada);
            }
            Console.WriteLine();
            Console.WriteLine("Pressione enter para continuar");
            Console.ReadLine();
        }

        else
        {
            Console.WriteLine("Nenhuma movimentação registrada");
            Console.WriteLine("Pressione enter para continuar");
            Console.ReadLine();
        }
    }
}
