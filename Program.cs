namespace Szunetdoga
{
    internal class Program
    {
        static List<int> Ar = new List<int>();
        static List<string> Nev = new List<string>();
        static List<string> Kategoria = new List<string>();
        static int Koltsegvetes = 0;
        static bool Allapot = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Add meg az ajándékozási költségvetésed: ");
            while (true)
            {
                try
                {
                    Koltsegvetes = Convert.ToInt32(Console.ReadLine());
                    if (Koltsegvetes < 0)
                        throw new Exception("Költségvetés nem lehet negatív!");
                        break;
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (Allapot)
            {
                Console.WriteLine("Válassz egyet: ");
                Console.WriteLine("1. Ajándék hozzáadása");
                Console.WriteLine("2. Ajándék szerkesztése");
                Console.WriteLine("3. Ajándék eltávolítása");
                Console.WriteLine("4. Ajándéklista megtekintése");
                Console.WriteLine("5. Költségvetés ellenőrzése");
                Console.WriteLine("6. Statisztikák megtekintése");
                Console.WriteLine("7. Kilépés");

                int valasztas = Convert.ToInt32(Console.ReadLine());    
                
                switch (valasztas)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Edit();
                        break;
                    case 3:
                        Remove();
                        break;
                    case 4:
                        ListaMegtekint();
                        break;
                    case 5:
                        Check();
                        break;
                    case 6:
                        Statisztika();
                        break;
                    case 7:
                        Allapot = false;
                        break;
                    default:
                        Console.WriteLine("Nem megfelelő választás!");
                        break;
                }

                static void Add()
                {
                    Console.WriteLine("Add meg az ajándék nevét: ");
                    string nev = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nev))
                        throw new Exception("Az ajándék neve nem lehet üres!");
                    
                    Console.WriteLine("Add meg az ajándék árát: ");
                    int ar = Convert.ToInt32(Console.ReadLine());
                    if (ar <= 0)
                        throw new Exception("Az árnak pozitívnak kell lennie!");

                    Console.WriteLine("Add meg az ajándék kategóriáját: ");
                    string kategoria = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(kategoria))
                        throw new Exception("A kategória nem lehet üres!");

                    Nev.Add(nev);
                    Ar.Add(ar);
                    Kategoria.Add(kategoria);
                    Console.WriteLine("Ajándék sikeresen hozzáadva!");
                }

                static void Edit()
                {
                    ListaMegtekint();

                    Console.WriteLine("Add meg az ajándék nevét: ");
                    try
                    {
                        int i = Convert.ToInt32(Console.ReadLine())-1;
                        if (i < 0 || i >= Nev.Count)
                            throw new Exception("Nem létező ajándék");

                        Console.WriteLine("Új név: (hagyd üresen ha nem akarod változtatni)");
                        string ujNev = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ujNev))
                            Nev[i] = ujNev;

                        Console.WriteLine("Új ár: (hagyd üresen ha nem akarod változtatni)");
                        string ujAr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ujAr))
                            Ar[i] = Convert.ToInt32(ujAr);

                        Console.WriteLine("Új kategória: (hagyd üresen ha nem akarod változtatni)");
                        string ujKategoria = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ujKategoria))
                            Kategoria[i] = ujKategoria;

                        Console.WriteLine("Ajándék sikeresen szerkesztve!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                static void Remove()
                {
                    ListaMegtekint();

                    Console.Write("Add meg a törlendő ajándék sorszámát: ");
                    try
                    {
                        int index = Convert.ToInt32(Console.ReadLine()) - 1;
                        if (index < 0 || index >= Nev.Count) throw new Exception("Érvénytelen sorszám!");

                        Nev.RemoveAt(index);
                        Ar.RemoveAt(index);
                        Kategoria.RemoveAt(index);
                        Console.WriteLine("Ajándék sikeresen törölve!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hiba: {ex.Message}");
                    }
                }

                static void ListaMegtekint()
                {
                    if (!Nev.Any())
                    {
                        Console.WriteLine("Nincs egyetlen ajándék sem a listában.");
                        return;
                    }

                    Console.WriteLine("Ajándéklista:");
                    for (int i = 0; i < Nev.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Nev[i]} - {Ar[i]} Ft - {Kategoria[i]}");
                    }
                }

                static void Check()
                {
                    int koltes = Ar.Sum();
                    int maradek = Koltsegvetes - koltes;

                    Console.WriteLine($"Elköltött összeg: {koltes} Ft");
                    Console.WriteLine($"Maradék költségvetés: {maradek} Ft");

                    if (maradek < 0)
                        Console.WriteLine("Figyelem: Túllépted a költségvetést!");
                }

                static void Statisztika()
                {
                    if (!Nev.Any())
                    {
                        Console.WriteLine("Nincs elég adat a statisztikákhoz.");
                        return;
                    }

                    Console.WriteLine($"Ajándékok száma: {Nev.Count}");
                    Console.WriteLine($"Ajándékok összértéke: {Ar.Sum()} Ft");

                    int max = Ar.Max();
                    int min = Ar.Min();
                    int maxI = Ar.IndexOf(max);
                    int minI = Ar.IndexOf(min);

                    Console.WriteLine($"Legdrágább ajándék: {Nev[maxI]} - {max} Ft");
                    Console.WriteLine($"Legolcsóbb ajándék: {Nev[minI]} - {min} Ft");
                }
            }
        }


    }
}


