// ReSharper disable InconsistentNaming
namespace Lab_2;

internal class ProductieLR
{
    public int Numar { get; set; }
    public string Stanga { get; set; } = "";
    public List<string> Dreapta { get; set; } = new();

    public static List<ProductieLR> GetProdctii()
    {
        return
        [
            new ProductieLR { Numar = 1, Stanga = "E", Dreapta = ["E", "+", "T"] },
            new ProductieLR { Numar = 2, Stanga = "E", Dreapta = ["T"] },
            new ProductieLR { Numar = 3, Stanga = "T", Dreapta = ["T", "*", "F"] },
            new ProductieLR { Numar = 4, Stanga = "T", Dreapta = ["F"] },
            new ProductieLR { Numar = 5, Stanga = "F", Dreapta = ["(", "E", ")"] },
            new ProductieLR { Numar = 6, Stanga = "F", Dreapta = ["id"] }
        ];
    }
}

internal class ElementStiva
{
    public string Simbol { get; set; } = "";
    public int Stare { get; set; } 
}

internal class Program
{
    private Dictionary<int, Dictionary<string, string>> TA = new ();
    private Dictionary<int, Dictionary<string, int>> TS = new();
    
    private static void Main()
    {
        Stack<ElementStiva> stiva = new();
        stiva.Push(new ElementStiva
        {
            Simbol = "$", 
            Stare = 0
        });

        var intrare = Console.ReadLine()!
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToList();
        
        intrare.Add("$");

        var pozitie = 0;

        while (true)
        {
            var stareCurenta = stiva.Peek().Stare;
            var simbolCurent = intrare[pozitie];
            
            // calc actiune in TA
            // daca actiunea incepe cu 'd' = deplasare
            // daca incepe cu 'r' = reducere
            // daca este 'acc' = return true
            // daca nu exista return false

            return;
        }
    }
}