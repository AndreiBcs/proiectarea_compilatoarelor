using System.Text;
using System.Text.Json;

namespace Lab_1;

internal class Gramatica
{
    public List<string> Neterminale { get; init; } = new();
    public List<string> Terminale { get; init; } = new();
    public string Start { get; init; } = "";
    public Dictionary<string, List<string>> Productii { get; init; } = new();
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Neterminale: {string.Join(", ", Neterminale)}");
        sb.AppendLine($"Terminale: {string.Join(", ", Terminale)}");
        sb.AppendLine($"Start: {Start}");
        sb.AppendLine("Productii: ");

        foreach (var kvp in Productii)
        {
            sb.AppendLine($"{kvp.Key} -> {string.Join(" | ", kvp.Value)}");
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
    private const int NumarSiruri = 10;
    
    public static void Main()
    {
        var gramatica = JsonSerializer.Deserialize<Gramatica>(File.ReadAllText(DataInPath))!;
        
        Console.WriteLine(gramatica + "\n\nApasa S pentru derivare extrem stanga; D pentru derivare extrem dreapta");

        var tipDerivare = CitesteTipDerivare();

        Console.WriteLine("\n\nSiruri generate:\n");

        for (var i = 0; i < NumarSiruri; i++)
        {
            Console.WriteLine(Deriveaza(gramatica, tipDerivare));
        }
    }
    
    private static TipDerivare CitesteTipDerivare()
    {
        while (true)
        {
            var tasta = Console.ReadKey(true).Key;
 
            if (tasta == ConsoleKey.S)
                return TipDerivare.ExtremStanga;
 
            if (tasta == ConsoleKey.D)
                return TipDerivare.ExtremDreapta;
        }
    }

    private static string Deriveaza(Gramatica gramatica, TipDerivare tipDerivare)
    {
        var sir = gramatica.Start;
        
        while (true)
        {
            if (sir.Length > LungimeMaxSir || !ContineNeterminale(sir, gramatica))
            {
                break;
            }

            switch (tipDerivare)
            {
                case TipDerivare.ExtremStanga:
                {
                    for (var i = 0; i < sir.Length; i++)
                    {
                        if (gramatica.Neterminale.Contains(sir[i].ToString()))
                        {
                            var productie = AlegeProductia(
                                sir[i].ToString(),
                                gramatica.Productii);

                            sir = sir.Remove(i, 1);
                            sir = sir.Insert(i, productie);
                            break;
                        }
                    }

                    break;
                }
                case TipDerivare.ExtremDreapta:
                {
                    for (var i = sir.Length - 1; i >= 0; i--)
                    {
                        if (gramatica.Neterminale.Contains(sir[i].ToString()))
                        {
                            var productie = AlegeProductia(
                                sir[i].ToString(),
                                gramatica.Productii);

                            sir = sir.Remove(i, 1);
                            sir = sir.Insert(i, productie);
                            break;
                        }
                    }

                    break;
                }
            }
        }
        
        return sir.Length == 0 ? "sir vid" : sir;
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