using BogusLibrary.Models;

namespace HumansApp;

/// <summary>
/// Represents a grouping of people by their gender.
/// </summary>
/// <remarks>
/// This record is used to group a collection of <see cref="Human"/> objects by their gender.
/// It contains the gender of the group and the list of people belonging to that group.
/// </remarks>
public record GenderPersonGrouped(Gender? Gender, List<Human> People);