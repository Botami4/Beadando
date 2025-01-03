using System;
using System.Collections.Generic;

namespace KaracsonyiAjandekTervezo
{
    class Program
    {
        static List<int> Ar = new List<int>();
        static List<string> Nev = new List<string>();
        static List<string> Kategoria = new List<string>();
        static int Koltsegvetes = 0;
        static bool Allapot = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Köszöntünk a Karácsonyi Ajándéktervezőben!");
            Console.Write("Add meg az ajándékozási költségvetésedet (Ft): ");

            while (true)
            {
                try
                {
                    Koltsegvetes = Convert.ToInt32(Console.ReadLine());
                    if (Koltsegvetes < 0) throw new Exception("A költségvetés nem lehet negatív!");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba: {ex.Message} Próbáld újra!");
                }
            }

            while (Allapot)
            {
                Console.WriteLine("\nVálassz egy lehetőséget:");
                Console.WriteLine("1. Ajándék hozzáadása");
                Console.WriteLine("2. Ajándék szerkesztése");
                Console.WriteLine("3. Ajándék eltávolítása");
                Console.WriteLine("4. Ajándéklista megtekintése");
                Console.WriteLine("5. Költségvetés ellenőrzése");
                Console.WriteLine("6. Statisztikák megtekintése");
                Console.WriteLine("7. Kilépés");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGift();
                        break;
                    case "2":
                        EditGift();
                        break;
                    case "3":
                        RemoveGift();
                        break;
                    case "4":
                        ViewGifts();
                        break;
                    case "5":
                        CheckBudget();
                        break;
                    case "6":
                        ShowStatistics();
                        break;
                    case "7":
                        Console.WriteLine("Kellemes karácsonyt!");
                        Allapot = false;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás. Próbáld újra!");
                        break;
                }
            }
        }

        static void AddGift()
        {
            try
            {
                Console.Write("Ajándék neve: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) throw new Exception("Az ajándék neve nem lehet üres!");

                Console.Write("Ajándék ára (Ft): ");
                int price = Convert.ToInt32(Console.ReadLine());
                if (price <= 0) throw new Exception("Az árnak pozitívnak kell lennie!");

                Console.Write("Ajándék kategóriája: ");
                string category = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(category)) throw new Exception("A kategória nem lehet üres!");

                Nev.Add(name);
                Ar.Add(price);
                Kategoria.Add(category);
                Console.WriteLine("Ajándék sikeresen hozzáadva!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }

        static void EditGift()
        {
            ViewGifts();
            Console.Write("Add meg a szerkesztendő ajándék sorszámát: ");
            try
            {
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                if (index < 0 || index >= Nev.Count) throw new Exception("Érvénytelen sorszám!");

                Console.Write("Új név (hagy üresen, ha nem változik): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    Nev[index] = newName;

                Console.Write("Új ár (Ft, hagyd üresen, ha nem változik): ");
                string newPriceInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPriceInput))
                {
                    int newPrice = Convert.ToInt32(newPriceInput);
                    if (newPrice <= 0) throw new Exception("Az árnak pozitívnak kell lennie!");
                    Ar[index] = newPrice;
                }

                Console.Write("Új kategória (hagy üresen, ha nem változik): ");
                string newCategory = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCategory))
                    Kategoria[index] = newCategory;

                Console.WriteLine("Ajándék sikeresen szerkesztve!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }

        static void RemoveGift()
        {
            ViewGifts();
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

        static void ViewGifts()
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

        static void CheckBudget()
        {
            int totalSpent = Ar.Sum();
            int remaining = Koltsegvetes - totalSpent;

            Console.WriteLine($"Eddig elköltött összeg: {totalSpent} Ft");
            Console.WriteLine($"Hátralévő költségvetés: {remaining} Ft");

            if (remaining < 0)
                Console.WriteLine("Figyelem: Túllépted a költségvetést!");
        }

        static void ShowStatistics()
        {
            if (!Nev.Any())
            {
                Console.WriteLine("Nincs elég adat a statisztikákhoz.");
                return;
            }

            Console.WriteLine($"Ajándékok száma: {Nev.Count}");
            Console.WriteLine($"Ajándékok összértéke: {Ar.Sum()} Ft");

            int maxPrice = Ar.Max();
            int minPrice = Ar.Min();
            int maxIndex = Ar.IndexOf(maxPrice);
            int minIndex = Ar.IndexOf(minPrice);

            Console.WriteLine($"Legdrágább ajándék: {Nev[maxIndex]} - {maxPrice} Ft");
            Console.WriteLine($"Legolcsóbb ajándék: {Nev[minIndex]} - {minPrice} Ft");
        }
    }
}
