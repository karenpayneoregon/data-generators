using BogusLibrary.Classes;
using BogusLibrary.Models;
using CommonHelpersLibrary;
using HumansApp.Classes;
using Spectre.Console;
using SpectreConsoleLibrary;
using System.Text.Json;

namespace HumansApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        PrintHumansBornBetween1950And1980();

        //GetFemaleHumansOrderedByFirstName();

        //GenerateAndDeserialize();

        //GroupAndDisplayHumansByGender();

        //TestingMaskingSocialSecurityProperty();
        
        SpectreConsoleHelpers.ExitPrompt();
    }

    /// <summary>
    /// Groups a collection of humans by their gender and displays each group along with the individuals' details.
    /// </summary>
    /// <remarks>
    /// This method generates a list of humans, groups them by their gender, and displays the grouped data
    /// in a formatted manner using Spectre.Console.
    /// </remarks>
    private static void GroupAndDisplayHumansByGender()
    {

        SpectreConsoleHelpers.PrintPink();
        
        var humans = HumanGenerator.Create(25);
        
        var result = humans
            .GroupBy(h => h.Gender)
            .Select(g => new GenderPersonGrouped(g.Key, g.OrderBy(h => h.LastName).ToList()))
            .ToList();

        var jsonGrouped = JsonSerializer.Serialize(result, Indented);

        File.WriteAllText("Json\\gender_grouped.json", jsonGrouped);

        // set a breakpoint on the following line to inspect the 'deserialized' variable
        var deserialized = JsonSerializer.Deserialize<List<GenderPersonGrouped>>(jsonGrouped, Indented);

        foreach (var group in result)
        {
            AnsiConsole.MarkupLine($"[bold yellow]Gender: {group.Gender}[/]");

            foreach (var person in group.People)
            {
                var age = person.BirthDate.GetAge();
                
                if (age == 0)
                {
                    AnsiConsole.MarkupLine($"  {person.FirstName, -10} {person.LastName, -15}[cyan]Born today[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"  {person.FirstName,-10} {person.LastName,-15}{age}");
                }
                    
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Retrieves a collection of female humans, orders them by their first name in ascending order,
    /// and prints the results to the console in a formatted style.
    /// </summary>
    /// <remarks>
    /// This method generates a list of humans, filters it to include only females, 
    /// sorts them by their first name, and displays the formatted output using Spectre.Console.
    /// </remarks>
    private static void GetFemaleHumansOrderedByFirstName()
    {

        SpectreConsoleHelpers.PrintPink();
        
        IOrderedEnumerable<Human> humans = HumanGenerator.Create(15)
            .Where(x => x.Gender == Gender.Female)
            .OrderBy(x => x.FirstName);

        foreach (var h in humans)
        {
            AnsiConsole.MarkupLine($"[bold green]{h.FirstName, -15}{h.LastName, -15}[/]");
        }
    }

    /// <summary>
    /// Prints a list of humans born between the years 1950 and 1980.
    /// </summary>
    /// <remarks>
    /// This method generates a collection of humans using the <see cref="HumanGenerator.Create"/> method
    /// and filters them based on their birth year. The filtered list is then displayed in the console
    /// with formatted output using Spectre.Console.
    /// </remarks>
    private static void PrintHumansBornBetween1950And1980()
    {
        SpectreConsoleHelpers.PrintPink();
        
        var humans = HumanGenerator.Create(15);

        foreach (var h in humans)
        {
            var year = h.BirthDay!.Value.Year;
            if (year is >= 1950 and <= 1980)
            {
                AnsiConsole.MarkupLine($"[bold green]{h.FirstName, -15}{h.LastName, -15}[/] born in [yellow]{year}[/]");
            }
        }
    }

    /// <summary>
    /// Generates a JSON file containing a list of humans, then deserializes the JSON content back into a list of <see cref="Human"/> objects.
    /// </summary>
    /// <remarks>
    /// This method demonstrates the process of serializing and deserializing data using JSON.
    /// The generated JSON file is saved to the "Json\\humans.json" path.
    /// </remarks>
    private static void GenerateAndDeserialize()
    {
        SpectreConsoleHelpers.PrintPink();

        var fileName = "Json\\humans.json";
        File.WriteAllText(fileName, JsonGenerator.HumansAsJson(10));

        // Set a breakpoint on the following line to inspect the 'humans' variable
        var humans = JsonSerializer.Deserialize<List<Human>>(File.ReadAllText(fileName));

    }

    /// <summary>
    /// Demonstrates the masking of Social Security Numbers (SSNs) for a collection of human objects.
    /// </summary>
    /// <remarks>
    /// This method generates a list of human objects, masks their Social Security Numbers using the
    /// <see cref="CommonHelpersLibrary.StringExtensions.MaskSsn"/> extension method, and prints the formatted
    /// details to the console. It also utilizes <see cref="SpectreConsoleLibrary.SpectreConsoleHelpers.PrintPink"/>
    /// to display a formatted message in pink color.
    /// </remarks>
    private static void TestingMaskingSocialSecurityProperty()
    {

        SpectreConsoleHelpers.PrintPink();
        
        var humans = HumanGenerator.Create(25);
        foreach (var h in humans)
        {
            Console.WriteLine($"{h.FirstName, -10} {h.LastName, -15}{h.SocialSecurityNumber.MaskSsn()}");
        }
    }

    /// <summary>
    /// Gets a <see cref="JsonSerializerOptions"/> instance configured to format JSON output with indentation.
    /// </summary>
    /// <value>
    /// A <see cref="JsonSerializerOptions"/> object with <see cref="JsonSerializerOptions.WriteIndented"/> set to <c>true</c>.
    /// </value>
    public static JsonSerializerOptions Indented => new() { WriteIndented = true };
}