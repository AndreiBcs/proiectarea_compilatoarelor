// ReSharper disable InconsistentNaming
namespace Lab_2;

internal class ProductieLR
{
    public int Numar { get; init; }
    public string Stanga { get; init; } = "";
    public List<string> Dreapta { get; init; } = new();

    public static List<ProductieLR> GetProductii()
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
    public int Stare { get; init; }
}

internal class Program
{
    private static readonly Dictionary<int, Dictionary<string, string>> TA = new()
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
    private static readonly  Dictionary<int, Dictionary<string, int>> TS = new()
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

        Console.WriteLine("introdu sirul de validat:");
        var inputLine = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputLine))
        {
            Console.WriteLine("sir invalid");
            return;
        }

        var intrare = inputLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        
        intrare.Add("$");

        var pozitieIntrare = 0;
        var productii = ProductieLR.GetProductii();

        while (true)
        {
            if (pozitieIntrare >= intrare.Count)
            {
                Console.WriteLine("eroare: s-a terminat sirul de intrare");
                return;
            }
            
            var stareCurenta = stiva.Peek().Stare;
            var simbolCurent = intrare[pozitieIntrare];
            
            if (!TA.TryGetValue(stareCurenta, out var value) 
                || !value.ContainsKey(simbolCurent))
            {
                Console.WriteLine("eroare: sintaxa invalida");
                return;
            }

            var actiune = TA[stareCurenta][simbolCurent];

            if (actiune == "acc")
            {
                Console.WriteLine("sirul este acceptat");
                return;
            }

            if (actiune.StartsWith('d'))
            {
                var stareNoua = int.Parse(actiune[1..]);
                
                stiva.Push(new ElementStiva
                {
                    Simbol = simbolCurent,
                    Stare = stareNoua
                });
                
                pozitieIntrare++;
                Console.WriteLine($"deplasare ({actiune}): s-a pus '{simbolCurent}' cu starea {stareNoua} pe stiva");
            }
            else if (actiune.StartsWith('r'))
            {
                var numarProductie = int.Parse(actiune[1..]);
                var productie = productii.First(p => p.Numar == numarProductie);

                var elementeDeSters = productie.Dreapta.Count;

                for (var i = 0; i < elementeDeSters; i++)
                {
                    stiva.Pop();
                }
                
                var stareAnterioara = stiva.Peek().Stare;

                if (!TS.TryGetValue(stareAnterioara, out var value1) ||
                    !value1.TryGetValue(productie.Stanga, out var stareSalt))
                {
                    Console.WriteLine($"eroare în TS pentru starea {stareAnterioara} si neterminalul {productie.Stanga}");
                    return;
                }

                stiva.Push(new ElementStiva
                {
                    Simbol = productie.Stanga,
                    Stare = stareSalt
                });

                Console.WriteLine($"reducere ({actiune}): s-a aplicat {productie.Stanga} -> " +
                                  $"{string.Join("", productie.Dreapta)}, salt la starea {stareSalt}");
            }
        }
    }
}