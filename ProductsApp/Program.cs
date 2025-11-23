using System.Diagnostics;
using BogusLibrary.Classes;
using BogusLibrary.Models;
using Spectre.Console;
using SpectreConsoleLibrary;
using System.Linq;
using System.Text.Json;

namespace ProductsApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        DisplayHighValueProducts();
        DisplayClothingProducts();
        GenerateAndDeserializeProducts();
        ImplicitOperator();
        SpectreConsoleHelpers.ExitPrompt();
    }

    /// <summary>
    /// Generates a JSON file containing a list of products, then deserializes the JSON data back into a list of product objects.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="JsonGenerator.ProductsAsJson(int, bool)"/> method to generate the JSON data
    /// and the <see cref="JsonSerializer.Deserialize{TValue}(string, JsonSerializerOptions?)"/> method to deserialize the data.
    /// The generated JSON file is saved to the <c>Json\products.json</c> path.
    /// </remarks>
    private static void GenerateAndDeserializeProducts()
    {
        SpectreConsoleHelpers.PrintPink();

        var fileName = "Json\\products.json";
        File.WriteAllText(fileName, JsonGenerator.ProductsAsJson(10));

        // Set a breakpoint on the following line to inspect the 'products' variable
        var products = JsonSerializer.Deserialize<List<Products>>(File.ReadAllText(fileName));

    }

    /// <summary>
    /// Displays a list of high-value products (products with a unit price greater than 100),
    /// sorted in descending order by their unit price.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of products using the <see cref="ProductGenerator.Create"/> method.
    /// It filters the products based on their unit price and displays them in the console using Spectre.Console.
    /// </remarks>
    private static void DisplayHighValueProducts()
    {
        SpectreConsoleHelpers.PrintPink();

        IOrderedEnumerable<Products> products = ProductGenerator.Create(15)
            .Where(x => x.UnitPrice > 100)
            .OrderByDescending(x => x.UnitPrice);

        foreach (var p in products)
        {
            AnsiConsole.MarkupLine($"[bold green]{p.ProductName,-25}[/][yellow]{p.UnitPrice:C}[/]");
        }
    }

    /// <summary>
    /// Displays a list of clothing products with their names and unit prices.
    /// </summary>
    /// <remarks>
    /// This method generates a list of products, filters them to include only those
    /// in the "Clothing" category, and displays the filtered products in a formatted
    /// manner using Spectre.Console.
    /// </remarks>
    private static void DisplayClothingProducts()
    {
        SpectreConsoleHelpers.PrintPink();

        List<Products> products = ProductGenerator.Create(10);

        List<Products> clothing = products
            .Where(p => p.Category.CategoryName == "Clothing")
            .ToList();

        foreach (var p in clothing)
        {
            AnsiConsole.MarkupLine($"[bold green]{p.ProductName,-25}[/][yellow]{p.UnitPrice:C}[/]");
        }

    }

    /// <summary>
    /// Demonstrates the usage of the implicit operator to convert a collection of 
    /// <see cref="BogusLibrary.Models.Products"/> instances into a collection of 
    /// <see cref="BogusLibrary.Models.ProductItem"/> instances.
    /// </summary>
    /// <remarks>
    /// This method utilizes the implicit conversion defined in the <see cref="BogusLibrary.Models.ProductItem"/> class
    /// to seamlessly transform <see cref="BogusLibrary.Models.Products"/> objects into <see cref="BogusLibrary.Models.ProductItem"/> objects.
    /// The resulting collection is then processed further.
    /// </remarks>
    private static void ImplicitOperator()
    {

        List<ProductItem> products = ProductGenerator.Create(10)
            .Select<Products, ProductItem>(p => p)
            .ToList();

        Debugger.Break();
    }

}
