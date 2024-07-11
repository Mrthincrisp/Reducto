using Reducto;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Linq;

namespace Reducto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ProductType> type =
                [
             new ProductType()
            {
                Id = "wan",
                Name = "Wand"
            },
            new ProductType()
            {
                Id = "app",
                Name = "Apparel"
            },
            new ProductType()
            {
                Id = "pot",
                Name = "Potion"
            },
            new ProductType()
            {
                Id = "enchant",
                Name = "Enchanted Object"
            }
            ];

            List<Products> products = 
            [
               new Products("Phoenix Core Wand", 12.25m, false, type.First(t => t.Id == "wan")),
               new Products("Dragonhide Armor", 75.50m, false, type.First(t => t.Id == "app")),
               new Products("Healing Potion", 8.00m, true, type.First(t => t.Id == "pot")),
               new Products("Invisibility Cloak", 150.00m, false, type.First(t => t.Id == "enchant"))
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
                        AddProduct(products);
                        break;
                    case "3":
                        DeleteProduct(products, type);
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
                    Console.WriteLine($"   {i + 1}. {products[i].Name} : ${products[i].Price}.");
                }
                    Console.Write("Press any key to continue.");
                    Console.ReadKey();
            }

            void DeleteProduct(List<Products> products, List<ProductType> type)
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

            void AddProduct(List<Products> products)
            {
                Console.WriteLine("Enter Product details: Product's name?");
                string? name = Console.ReadLine();

                Console.WriteLine("Enter Product details: Product's Price?");
                decimal price;
                while (!decimal.TryParse(Console.ReadLine(), out price)) 
                {
                    Console.WriteLine("that is not a valid number please enter a format of 1.23");
                }

                bool sold = false;

                Console.WriteLine("enter the type's number");
                for (int i = 0; i < type.Count; i++)
                {
                    Console.WriteLine($@"   {i}: {type[i].Name}");
                }

                int typeIndex;
                while (!int.TryParse(Console.ReadLine(), out typeIndex) || typeIndex < 0 || typeIndex >= type.Count)
                {
                    Console.WriteLine("Invalid input, try again.");
                }

                Products newProduct = new(name, price, sold, type[typeIndex]);
                products.Add(newProduct);
                Console.WriteLine($"  {newProduct.Name} was added");
                Console.Write("  press any key to continue");
                Console.ReadKey();
            }
        }
    }
}




