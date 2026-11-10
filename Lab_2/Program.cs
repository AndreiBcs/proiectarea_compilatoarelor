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
    private Dictionary<int, Dictionary<string, string>> TA = new()
    {
        { 0, new Dictionary<string, string>
            {
                { "id", "d5" },
                { "(", "d4" }
            }
        },
        { 1, new Dictionary<string, string>
            {
                { "+", "d6" },
                { "$", "acc" }
            }
        },
        { 2, new Dictionary<string, string>
            {
                { "+", "r2" },
                { "*", "d7" },
                { ")", "r2" },
                { "$", "r2" }
            }
        },
        { 3, new Dictionary<string, string>
            {
                { "+", "r4" },
                { "*", "r4" },
                { ")", "r4" },
                { "$", "r4" }
            }
        },
        { 4, new Dictionary<string, string>
            {
                { "id", "d5" },
                { "(", "d4" }
            }
        },
        { 5, new Dictionary<string, string>
            {
                { "+", "r6" },
                { "*", "r6" },
                { ")", "r6" },
                { "$", "r6" }
            }
        },
        { 6, new Dictionary<string, string>
            {
                { "id", "d5" },
                { "(", "d4" }
            }
        },
        { 7, new Dictionary<string, string>
            {
                { "id", "d5" },
                { "(", "d4" }
            }
        },
        { 8, new Dictionary<string, string>
            {
                { "+", "d6" },
                { ")", "d11" }
            }
        },
        { 9, new Dictionary<string, string>
            {
                { "+", "r1" },
                { "*", "d7" },
                { ")", "r1" },
                { "$", "r1" }
            }
        },
        { 10, new Dictionary<string, string>
            {
                { "+", "r3" },
                { "*", "r3" },
                { ")", "r3" },
                { "$", "r3" }
            }
        },
        { 11, new Dictionary<string, string>
            {
                { "+", "r5" },
                { "*", "r5" },
                { ")", "r5" },
                { "$", "r5" }
            }
        }
    };
    private Dictionary<int, Dictionary<string, int>> TS = new()
    {
        { 0, new Dictionary<string, int>
            {
                { "E", 1 },
                { "T", 2 },
                { "F", 3 }
            }
        },
        { 1, new Dictionary<string, int>() },
        { 2, new Dictionary<string, int>() },
        { 3, new Dictionary<string, int>() },
        { 4, new Dictionary<string, int>
            {
                { "E", 8 },
                { "T", 2 },
                { "F", 3 }
            }
        },
        { 5, new Dictionary<string, int>() },
        { 6, new Dictionary<string, int>
            {
                { "T", 9 },
                { "F", 3 }
            }
        },
        { 7, new Dictionary<string, int>
            {
                { "F", 10 }
            }
        },
        { 8, new Dictionary<string, int>() },
        { 9, new Dictionary<string, int>() },
        { 10, new Dictionary<string, int>() },
        { 11, new Dictionary<string, int>() }
    };
    
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