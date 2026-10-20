using System.Text.Json;

namespace Lab_1;

internal struct Gramatica
{
    public List<string> Neterminale { get; init; }
    public List<string> Terminale { get; init; }
    public string Start { get; init; }
    public Dictionary<string, List<string>> Productii { get; init; }
    public override string ToString()
    {
        string result = "Neterminale: ";
        
        foreach (var n in Neterminale)
        {
            result += n + ", ";
        }
        
        result += "\nTerminale: ";
        
        foreach (var t in Terminale)
        {
            result += t + ", ";
        }

        result += $"\nStart: {Start}\nProductii: ";

        foreach (var kvp in Productii)
        {
            result += $"{kvp.Key}: ";
                
            foreach (var value in kvp.Value)
            {
                result += $"{value}, ";
            }
        }
        
        return result;
    }
}

internal class Program
{
    private const string DataInPath = "Data/gramatica.in.json";
    
    public static void Main(string[] args)
    {
        string json = File.ReadAllText(DataInPath);
        Gramatica gramatica = JsonSerializer.Deserialize<Gramatica>(json);

        Console.WriteLine(gramatica.ToString());
        
        
    }
}