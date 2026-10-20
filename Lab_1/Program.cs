using System.Text;
using System.Text.Json;

namespace Lab_1;

internal enum TipDerivare
{
    ExtremStanga,
    ExtremDreapta
}

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
    private const string SirVid = "λ";
    private const int LungimeMaxSir = 60;
    private const int NumarSiruri = 10;
    
    public static void Main()
    {
        var gramatica = JsonSerializer.Deserialize<Gramatica>(File.ReadAllText(DataInPath));

        Console.WriteLine(gramatica + "\n\nApasa S pentru derivare extrem stanga; D pentru derivare extrem dreapta");

        var tip = CitesteTipDerivare();
        
        Console.WriteLine("\n\nSiruri generate:\n");

        for (var i = 0; i < NumarSiruri; i++)
        {
            if (gramatica != null)
            {
                Console.WriteLine(Deriveaza(gramatica, tip));
            }
        }
    }

    private static TipDerivare CitesteTipDerivare()
    {
        while (true)
        {
            var tasta = Console.ReadKey().Key;
 
            if (tasta == ConsoleKey.S)
                return TipDerivare.ExtremStanga;
 
            if (tasta == ConsoleKey.D)
                return TipDerivare.ExtremDreapta;
        }
    }
    
    private static string Deriveaza(Gramatica gramatica, TipDerivare tip)
    {
        var sir = gramatica.Start;
 
        while (sir.Length <= LungimeMaxSir)
        {
            var neterminale = GasesteNeterminale(sir, gramatica);
 
            // sirul contine doar terminale
            if (neterminale.Count == 0)
                return sir == "" ? SirVid : sir;
 
            var ales = tip == TipDerivare.ExtremStanga
                ? neterminale[0]
                : neterminale[neterminale.Count - 1];
 
            if (!gramatica.Productii.TryGetValue(ales.Nume, out var variante) || variante.Count == 0)
                return sir + " (oprit: neterminal fara productii)";
 
            var productie = variante[Random.Shared.Next(variante.Count)];
            if (productie == SirVid)
                productie = "";
 
            sir = sir.Remove(ales.Pozitie, ales.Nume.Length).Insert(ales.Pozitie, productie);
        }
 
        // lungime > 60 (sirul inca mai contine neterminale)
        return sir + " (oprit: lungime > 60)";
    }
 
    // returneaza toate neterminalele din sir, de la stanga la dreapta,
    // impreuna cu pozitia la care apar
    private static List<(int Pozitie, string Nume)> GasesteNeterminale(string sir, Gramatica gramatica)
    {
        var gasite = new List<(int Pozitie, string Nume)>();
        var index = 0;
 
        while (index < sir.Length)
        {
            var nume = NeterminalLa(sir, index, gramatica);
 
            if (nume == "")
            {
                index++;
            }
            else
            {
                gasite.Add((index, nume));
                index += nume.Length;
            }
        }
 
        return gasite;
    }
 
    // daca la pozitia data incepe un neterminal, il returneaza (cel mai lung posibil), altfel returneaza ""
    private static string NeterminalLa(string sir, int pozitie, Gramatica gramatica)
    {
        var rezultat = "";
 
        foreach (var neterminal in gramatica.Neterminale)
        {
            if (neterminal.Length > rezultat.Length &&
                sir.Substring(pozitie).StartsWith(neterminal, StringComparison.Ordinal))
            {
                rezultat = neterminal;
            }
        }
 
        return rezultat;
    }
}