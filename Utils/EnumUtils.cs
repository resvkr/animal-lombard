namespace AnimalLombard.Utils;

public static class EnumUtils
{
    public static TEnum AskEnumValue<TEnum>(string prompt) where TEnum : struct, Enum
    {
        Console.WriteLine(prompt);
        var enumValues = Enum.GetValues<TEnum>().ToList();

        for (var i = 0; i < enumValues.Count; i++)
        {
            Console.WriteLine($"{i}: {enumValues[i]}");
        }
        
        Console.WriteLine("Enter the name or index of the value: ");
        var input = Console.ReadLine();
        
        if (Enum.TryParse<TEnum>(input, true, out var parsedByName))
            return parsedByName;
        
        if (int.TryParse(input, out var index) && index >= 0 && index < enumValues.Count)
            return enumValues[index];
        
        throw new ArgumentException($"Invalid input: {input}. Please enter a valid name or index.");
    }
}