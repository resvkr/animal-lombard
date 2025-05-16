using System.ComponentModel.DataAnnotations;

namespace AnimalLombard.Modals;

public abstract class Animal
{
    private static long _nextId = 1;
    [Key] public string Id { get; private set; }

    [Required] public string Name { get; set; }

    [Required] public AnimalType AnimalType { get; set; }

    [Required] public string Species { get; set; }

    protected Animal(string name, AnimalType animalType, string species)
    {
        Id = _nextId++.ToString();
        Name = name;
        AnimalType = animalType;
        Species = species;
    }

    public override string ToString()
    {
        return $"Animal({Id}): {Name}, Type: {AnimalType.ToString()}, Species: {Species}";
    }
}