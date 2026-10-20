using System.Text;
using System.Text.Json;

namespace Lab_1;

internal struct Gramatica
{
    public TipDerivare TipDerivare { get; set; }
    public List<string> Neterminale { get; init; }
    public List<string> Terminale { get; init; }
    public string Start { get; init; }
    public Dictionary<string, List<string>> Productii { get; init; }
    public override string ToString()
    {
        string neterminale = string.Join(", ", Neterminale);
        string terminale = string.Join(", ", Terminale);
    
        var sb = new StringBuilder();
        sb.AppendLine($"Neterminale: {neterminale}");
        sb.AppendLine($"Terminale: {terminale}");
        sb.AppendLine($"Start: {Start}");
        sb.Append("Productii: ");

        foreach (var kvp in Productii)
        {
            sb.Append($"{kvp.Key} -> {string.Join(" | ", kvp.Value)}; ");
        }
    
        return sb.ToString();
    }
}

internal enum TipDerivare
{
    ExtremStanga,
    ExtremDreapta
}

internal class Program
{
    private const string DataInPath = "Data/gramatica.in.json";
    private const string SirVid = "λ";
    private const int LungimeMaxSir = 60;
    
    public static void Main(string[] args)
    {
        string json = File.ReadAllText(DataInPath);
        Gramatica gramatica = JsonSerializer.Deserialize<Gramatica>(json);

        Console.WriteLine(gramatica.ToString());
        Console.WriteLine();
        Console.WriteLine("Apasa S pentru derivare extrem stanga; D pentru derivare extrem dreapta");

        var derivare = Console.ReadKey();
        Console.WriteLine();
        
        switch (derivare.Key)
        {
            case ConsoleKey.S: gramatica.TipDerivare = TipDerivare.ExtremStanga;
                break;
            case ConsoleKey.D: gramatica.TipDerivare = TipDerivare.ExtremDreapta;
                break;
        }

        string sir = gramatica.Start;
        while (true)
        {
            if (sir.Length > LungimeMaxSir || !ContineNeterminale(sir, gramatica))
            {
                break;
            }

            switch (gramatica.TipDerivare)
            {
                case TipDerivare.ExtremStanga:
                {
                    for (int i = 0; i < sir.Length; i++)
                    {
                        if (gramatica.Neterminale.Contains(sir[i].ToString()))
                        {
                            var productie = AlegeProductia(
                                sir[i].ToString(),
                                gramatica.Productii);

                            sir = sir.Replace(sir[i].ToString(), productie);
                            break;
                        }
                    }

                    break;
                }
                case TipDerivare.ExtremDreapta:
                {
                    for (int i = sir.Length - 1; i >= 0; i--)
                    {
                        if (gramatica.Neterminale.Contains(sir[i].ToString()))
                        {
                            var productie = AlegeProductia(
                                sir[i].ToString(),
                                gramatica.Productii);

                            sir = sir.Replace(sir[i].ToString(), productie);
                            break;
                        }
                    }

                    break;
                }
            }
        }
        
        Console.WriteLine(sir.Length == 0 ? "Sirul vid" : $"Sirul generat: {sir}");
    }

    private static bool ContineNeterminale(string sir, Gramatica gramatica)
    {
        foreach (var neterminal in gramatica.Neterminale) 
        {
            if (sir.Contains(neterminal))
            {
                return true;
            }
        }
        return false;
    }

    private static string AlegeProductia(
        string neterminal,
        Dictionary<string, List<string>> productii)
    {
        var randomKey = Random.Shared.Next(0, productii[neterminal].Count);
        
        return productii[neterminal][randomKey] == SirVid 
            ? "" 
            : productii[neterminal][randomKey];
    }
}