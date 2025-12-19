using System.Diagnostics;
using BogusLibrary.Classes;
using BogusLibrary.Models;
using Spectre.Console;
using SpectreConsoleLibrary;
using System.Linq;
using System.Text.Json;
using ProductsApp.Classes;

namespace ProductsApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        
        Examples.DisplayHighValueProducts();
        Examples.DisplayClothingProducts();
        Examples.GenerateAndDeserializeProducts();
        Examples.ImplicitOperator();
        
        SpectreConsoleHelpers.ExitPrompt();
    }


}
