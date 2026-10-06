using Classes;
using System.Linq;
using Newtonsoft.Json;

using (StreamReader r = new StreamReader("./vendas.json"))
{
    string json = r.ReadToEnd();
    VendasJson? vendedoresJson = !string.IsNullOrEmpty(json) ? JsonConvert.DeserializeObject<VendasJson>(json) : null;

    if ( vendedoresJson != null && vendedoresJson.Vendas.Count > 0 )
    {
        int id = 1;
        var vendedores = vendedoresJson.Vendas.DistinctBy(v => v.Vendedor).Select(
            x =>
            new Vendedor
            {
                Id = id++,
                Nome = x.Vendedor,
                Vendas = vendedoresJson.Vendas
                    .Where(y => y.Vendedor == x.Vendedor)
                    .Select(v => new VendasVendedor 
                        { 
                            Id = Guid.NewGuid(), 
                            Valor = v.Valor, 
                            Comissao = v.Valor < 100 ? 0 : v.Valor < 500 ? v.Valor * 0.01M : v.Valor * 0.05M
                        })
                    .ToList(),
                ComissaoTotal = vendedoresJson.Vendas
                    .Where(y => y.Vendedor == x.Vendedor)
                    .Sum(x => 
                        x.Valor < 100 ? 0 : 
                        x.Valor < 500 ? (x.Valor * 0.01M) :
                        x.Valor * 0.05M)
            });

        Console.WriteLine(".:Comissões:.");
        foreach( var vendedor in vendedores)
        {
            Console.WriteLine($"Vendedor: {vendedor.Id}");
            Console.WriteLine($"Nome: {vendedor.Nome}");
            Console.WriteLine();
            Console.WriteLine("Vendas");

            foreach (var venda in vendedor.Vendas)
            {
                Console.WriteLine($"Valor: {venda.Valor} - Comissão: {venda.Comissao.ToString("F")}");
            }

            Console.WriteLine();
            Console.WriteLine($"Comissão total: {vendedor.ComissaoTotal.ToString("F")}");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}