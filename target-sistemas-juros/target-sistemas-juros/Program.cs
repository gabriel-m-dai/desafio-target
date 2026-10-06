Console.Write("Informe o valor: ");
var valorInput = Console.ReadLine();
var valor = 0d;

var parse = double.TryParse(valorInput, out valor);

while (!parse)
{
    Console.Write("Informe o valor: ");
    valorInput = Console.ReadLine();

    parse = double.TryParse(valorInput, out valor);
}

Console.Write("Informe a data (dd/MM/aaaa): ");
var dataInput = Console.ReadLine();
DateTime data;

parse = DateTime.TryParse(dataInput, out data);

while (!parse)
{
    Console.Write("Informe a data: ");
    dataInput = Console.ReadLine();

    parse = DateTime.TryParse(dataInput, out data);
}

var dias = data.Subtract(DateTime.Today).Days;
var taxa = 1 + 0.025;
var taxaPeriodo = Math.Pow(taxa, dias);

var montante = valor * taxaPeriodo;
var juros = montante - valor;

Console.WriteLine($"Juros: {juros.ToString("F")}");
