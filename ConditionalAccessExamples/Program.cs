using BogusLibrary.Classes;
using BogusLibrary.Models;
using Spectre.Console;

namespace ConditionalAccessExamples;
internal partial class Program
{
    static void Main(string[] args)
    {
        NoviceExample();
        TestForNull();
        TestForNullWithConditionalAccess();
        HasAddress();

        ExitPrompt(Justify.Left);
    }

    private static void NoviceExample()
    {
        PrintPink();

        Human human = new() { FirstName = "Marlon" };

        try
        {
            Console.WriteLine($"Hello, {human.FirstName,-12}{human.Address.City}");
        }
        catch (Exception exception)
        {
            AnsiConsole.MarkupLine($"In Catch [red bold]{exception.Message}[/]");
        }

        Console.WriteLine();
        
    }

    private static void TestForNull()
    {

        PrintPink();

        Human human = new() { FirstName = "Marlon" };

        if (human.Address is not null && !string.IsNullOrWhiteSpace(human.Address.City))
        {
            Console.WriteLine($"Hello, {human.FirstName,-12}{human.Address.City}");
        }
        else
        {
            Console.WriteLine($"Hello, {human.FirstName,-12}City is not available.");
        }

        Console.WriteLine();
        
    }

    private static void TestForNullWithConditionalAccess()
    {
        
        PrintPink();
        
        Human human = new() { FirstName = "Marlon" };

        // The null conditional operator (?.) allows you to safely access members.
        var city = human.Address?.City;

        /*
         * The null-coalescing operator ?? returns the value of its left-hand operand if it's not null.
         * Otherwise, it evaluates the right-hand operand and returns its result.
         */
        Console.WriteLine($"Hello, {human.FirstName,-12}{human.Address?.City ?? "City is not available."}");

        Console.WriteLine();

    }

    private static void HasAddress()
    {

        PrintPink();

        // create a human with an address
        var human = HumanGenerator.CreateOne();
        
        Console.WriteLine($"Hello, {human.FirstName,-12}{human.Address?.City ?? "City is not available."}");

        Console.WriteLine();

    }

}

