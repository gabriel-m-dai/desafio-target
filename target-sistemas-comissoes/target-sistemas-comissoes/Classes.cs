using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Vendedor
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal ComissaoTotal { get; set; }
        public List<VendasVendedor> Vendas { get; set; } = [];
    }

    public class VendasVendedor
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public decimal Comissao { get; set; }
    }

    public class VendasJson
    {
        public List<VendedorDeserializado> Vendas { get; set; } = [];
    }

    public class VendedorDeserializado
    {
        public string Vendedor { get; set; }
        public decimal Valor { get; set; }
    }
}
