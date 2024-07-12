using Reducto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
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
               new Products("Phoenix Core Wand", 12.25m, false, type.First(t => t.Id == "wan"),new DateTime(2023, 5, 1)),
               new Products("Boots with the fur", 75.50m, false, type.First(t => t.Id == "app"),new DateTime(2024, 1, 1)),
               new Products("Healing Potion", 8.00m, true, type.First(t => t.Id == "pot"),new DateTime(2024, 2, 11)),
               new Products("Love Potion", 69.00m, true, type.First(t => t.Id == "pot"),new DateTime(2024, 2, 11)),
               new Products("Invisibility Cloak", 150.00m, false, type.First(t => t.Id == "enchant"),new DateTime(2024, 3, 20)),
               new Products("Flying Broom", 19.99m, false, type.First(t => t.Id == "enchant"),new DateTime(2024, 7, 10))
            ];

            string greeting = "Hey, here's the Main Menu:";

            Console.WriteLine(greeting);
            string? choice = null;
            while (choice != "0")
            {
                Console.WriteLine(@"  1. View all products
  2. Add a new product
  3. Delete a product
  4. Update a product
  5. Show available products
  6. Search for product Types
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
                        DeleteProduct(products);
                        break;
                    case "4":
                        UpdateProduct(products);
                        break;
                    case "5":
                        ShowAvailableProducts(products);
                        break;
                    case "6":
                        SearchType(products);
                        break;
                    default:
                        Console.WriteLine("PLEASE MAKE A SELECTION BETWEEN 0-4");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }

            void ShowAllProducts()
            {
                for (int i = 0; i < products.Count; i++){
                    Console.WriteLine($"   {i + 1}. {products[i].Name} : ${products[i].Price}, has been on shelf for {products[i].DaysOnShelf}days. {(products[i].Sold ? "Sold out" : "In Stock")}.");
                }
                    Console.WriteLine("Press any key to continue.");
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
                        Console.WriteLine("press any key to continue");
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

                DateTime dateStocked = DateTime.Now;

                Products newProduct = new(name, price, sold, type[typeIndex], dateStocked);
                products.Add(newProduct);
                Console.WriteLine($"  {newProduct.Name} was added");
                Console.WriteLine("  press any key to continue");
                Console.ReadKey();
            }

            void UpdateProduct(List<Products> products)
            {
                Console.WriteLine("Select a product to update, or enter q, to return to the main menu:");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"For {products[i].Name} enter {i}.");
                }
                string? input = Console.ReadLine();

                if (input == "q")
                {
                    return;
                }

                if (int.TryParse(input, out int index) && index >= 0 && index < products.Count)
                {
                    Console.WriteLine($"Enter the new name of product: {products[index].Name}, or press enter to leave unchanged");
                    string? newName = Console.ReadLine();

                    if (string.IsNullOrEmpty(newName))
                    {
                        newName = products[index].Name;
                    }

                    Console.WriteLine("Enter the new price of the product:");
                    decimal newPrice;
                    while (!decimal.TryParse(Console.ReadLine(), out newPrice))
                    {
                        Console.WriteLine("that is not a valid number please enter a format of 1.23");
                    }
                    newPrice = Math.Round(newPrice, 2);


                    bool newSold = false;
                    bool inputValid = false;

                    while (!inputValid)
                    {
                        Console.WriteLine("Is the current product Sold Out yes or no?:");
                        string? soldInput = Console.ReadLine()?.Trim().ToLower();

                        switch (soldInput)
                        {
                            case "yes":
                            case "y":
                                newSold = true;
                                inputValid = true;
                                break;
                            case "no":
                            case "n":
                                newSold = false;
                                inputValid = true;
                                break;
                            default:
                                Console.WriteLine("Invalid response. Please enter 'yes' or 'no'.");
                                break;
                        }
                    }


                    Console.WriteLine("enter the product's type number");
                    for (int i = 0; i < type.Count; i++)
                    {
                        Console.WriteLine($"   {i}: {type[i].Name}");
                    }

                    int newTypeIndex;
                    while (!int.TryParse(Console.ReadLine(), out newTypeIndex) || newTypeIndex < 0 || newTypeIndex >= type.Count)
                    {
                        Console.WriteLine("Invalid input, try again.");
                    }

                    products[index].Name = newName;
                    products[index].Price = newPrice;
                    products[index].Sold = newSold;
                    products[index].ProductType = type[newTypeIndex];

                    Console.WriteLine($"{newName} updated");
                    Console.WriteLine("  press any key to continue");
                    Console.ReadKey();
                }
            }

            void ShowAvailableProducts(List<Products> products)
            {
                Console.WriteLine("available items");
                List<Products> availableProducts = products.Where(p => !p.Sold).ToList();
                int index = 1;
                foreach (Products product in availableProducts) 
                {
                Console.WriteLine($"{index}. {product.Name}");
                    index++;
                }
                Console.WriteLine("  press any key to continue");
                Console.ReadKey();
            }

            void SearchType(List<Products> products)
            {
                bool check = false;
                while (!check) {
                Console.WriteLine("Which type would you like to search for?");
                Console.WriteLine(@"        1. Potions 
        2. Apperal
        3. Enchanted Objects
        4. wands");
                string? selection = Console.ReadLine()?.Trim().ToLower();
                    switch (selection)
                    {
                        case "1":
                        case "potions":
                        case "pots":
                        case "potion":
                        case "pot":
                            selection = "pot";
                            List<Products> potionProducts = products.Where(p => p.ProductType?.Id == selection).ToList();
                            foreach (Products product in potionProducts)
                            {
                                Console.WriteLine($"{product.Name}");
                            }
                            check = true;
                            Console.WriteLine("  press any key to continue");
                            Console.ReadKey();
                            break;
                        case "2":
                        case "app":
                        case "apperal":
                            selection = "app";
                            List<Products> apperalProducts = products.Where(p => p.ProductType?.Id == selection).ToList();
                            foreach (Products product in apperalProducts)
                            {
                                Console.WriteLine($"{product.Name}");
                            }
                            check = true;
                            Console.WriteLine("  press any key to continue");
                            Console.ReadKey();
                            break;
                        case "3":
                        case "ench":
                        case "enchanted objects":
                        case "enchanted object":
                        case "enchant":
                            selection = "enchant";
                            List<Products> enchantProducts = products.Where(p => p.ProductType?.Id == selection).ToList();
                            foreach (Products product in enchantProducts)
                            {
                                Console.WriteLine($"{product.Name}");
                            }
                            check = true;
                            Console.WriteLine("  press any key to continue");
                            Console.ReadKey();
                            break;
                        case "4":
                        case "wan":
                        case "wand":
                        case "wands":
                            selection = "wan";
                            List<Products> wandProducts = products.Where(p => p.ProductType?.Id == selection).ToList();
                            foreach (Products product in wandProducts)
                            {
                                Console.WriteLine($"{product.Name}");
                            }
                            check = true;
                            Console.WriteLine("  press any key to continue");
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine("invalid input.");
                            break;
                    }
                }
            }
        }
    }
}




