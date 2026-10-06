using System;
using System.Collections.Generic;
using System.Text;

namespace target_sistemas_estoque
{
    public class Produto
    {
        public int CodigoProduto { get; set; }
        public required string DescricaoProduto { get; set; }
        public int Estoque { get; set; }
    }

    public class EstoqueJson
    {
        public List<Produto> Estoque { get; set; } = [];
    }
}
