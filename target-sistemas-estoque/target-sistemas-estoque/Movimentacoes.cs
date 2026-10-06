using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace target_sistemas_estoque
{
    public class Movimentacoes
    {
        public Guid Id { get; set; }
        public int CodigoProduto { get; set; }
        public MovimentacoesEnum Descricao { get; set; }
        public int QuantidadeMovimentada { get; set; }
    }

    public enum MovimentacoesEnum
    {
        [Description("Entrada")]
        Entrada = 1,
        [Description("Saída")]
        Saida = 2
    }
}
