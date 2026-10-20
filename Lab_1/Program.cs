using System.Text.Json;

namespace Lab_1;

internal struct Gramatica
{
    public Program.TipDerivare TipDerivare { get; set; }
    public List<string> Neterminale { get; init; }
    public List<string> Terminale { get; init; }
    public string Start { get; init; }
    public Dictionary<string, List<string>> Productii { get; init; }
    public override string ToString()
    {
        string neterminale = string.Join(", ", Neterminale);
        string terminale = string.Join(", ", Terminale);
    
        var sb = new System.Text.StringBuilder();
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

internal class Program
{
    private const string DataInPath = "Data/gramatica.in.json";
    public enum TipDerivare
    {
        ExtremStanga,
        ExtremDreapta
    }
    
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
    }
}