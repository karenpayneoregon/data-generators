using Bogus;
using BogusLibrary.Models;

namespace BogusLibrary.Classes;

/// <summary>
/// Provides functionality for generating random address data using the Bogus library.
/// </summary>
/// <remarks>
/// This class is designed to create a collection of <see cref="Address"/> objects with randomly populated fields.
/// It leverages the Bogus library to generate realistic address data, including street names, cities, states, and more.
/// </remarks>
public static class AddressGenerator
{
    /// <summary>
    /// Generates a list of <see cref="Address"/> objects with randomly populated data.
    /// </summary>
    /// <param name="count">
    /// The number of <see cref="Address"/> objects to generate. Defaults to 10 if not specified.
    /// </param>
    /// <returns>
    /// A list of <see cref="Address"/> objects with randomly generated data.
    /// </returns>
    public static List<Address> Create(int count = 1)
    {

        Randomizer.Seed = new Random(337);

        var faker = new Faker<Address>()
            .RuleFor(a => a.Id, f => f.IndexFaker + 1)
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.State, f => f.Address.State())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Country, f => f.Address.Country());

        return faker.Generate(count);
    }

    /// <summary>
    /// Generates a single <see cref="Address"/> object with randomly populated data.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="Create(int)"/> method to generate a single <see cref="Address"/> object.
    /// It is a convenient shorthand for generating one address without specifying a count.
    /// </remarks>
    /// <returns>
    /// A single <see cref="Address"/> object with randomly generated data.
    /// </returns>
    public static Address CreateOne()
        => Create().FirstOrDefault()!;
}
