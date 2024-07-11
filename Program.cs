using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace Reducto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var wand = new ProductType
            {
                Id = "wan",
                Name = "Wand"
            };
            var apparel = new ProductType
            {
                Id = "app",
                Name = "Apparel"
            };
            var potion = new ProductType
            {
                Id = "pot",
                Name = "Potion"
            };
            var enchant = new ProductType
            {
                Id = "enchant",
                Name = "Enchanted Object"
            };

            List<Products> products =
            [
                new Products()
                {
                    Name = "Pheonix Core Wand",
                    Price = 12.25m,
                    Sold = false,
                    ProductType = wand
                },
                new Products()
                {
                    Name = "Blue Cloak",
                    Price = 25.05m,
                    Sold = false,
                    ProductType = apparel
                },
                new Products()
                {
                    Name = "Love Potion",
                    Price = 30.27m,
                    Sold = false,
                    ProductType = potion
                },
                new Products()
                {
                    Name = "Boots with the fur",
                    Price = 49.99m,
                    Sold = false,
                    ProductType = apparel
                },
                new Products()
                {
                    Name = "Flying Broom",
                    Price = 14.99m,
                    Sold = true,
                    ProductType = enchant
                }
            ];

            string greeting = "Hey, here's the Main Menu:";

            Console.WriteLine(greeting);
            string? choice = null;
            while (choice != "0")
            {
                Console.WriteLine(@"1. View all products
2. Add a new Product
3. Delete a Product
4. Update a Product
0. Quit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.WriteLine("Bye.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "1":
                        ShowAllProducts();
                        break;
                    case "2":
                        //
                        break;
                    case "3":
                        DeleteProduct(products);
                        break;
                    case "4":
                        //
                        break;
                    default:
                        Console.WriteLine("PLEASE MAKE A SELECTION BETWEEN 0-4");
                        Console.Write("Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }

            void ShowAllProducts()
            {
                for (int i = 0; i < products.Count; i++){
                    Console.WriteLine($"{i + 1}. {products[i].Name} : ${products[i].Price}.");
                }
                    Console.Write("Press any key to continue.");
                    Console.ReadKey();
            }

            void DeleteProduct(List<Products> products)
            {
                while (true)
                {
                    Console.WriteLine("Enter product number to delete, or press q to return.");
                    for (int i = 0; i < products.Count; i++)
                    {
                        Console.WriteLine($"To delete {products[i].Name} enter {i}.");
                    }
                    string? input = Console.ReadLine();

                    if (input == "q")
                    {
                        return;
                    }

                    if (int.TryParse(input, out int index) && index >= 0 && index < products.Count)
                    {
                        Console.WriteLine($"product {products[index].Name} has been deleted.");
                        products.RemoveAt(index);
                        Console.Write("press any key to continue");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Console.Write("press any key to return to the main menu");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
