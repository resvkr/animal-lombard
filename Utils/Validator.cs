using System.ComponentModel.DataAnnotations;

namespace AnimalLombard.Utils;

public static class ValidatorUtils
{
    public static void ValidateEntity(object entity)
    {
        var context = new ValidationContext(entity);
        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(entity, context, results, true);

        if (isValid) return;
        var errorMessages = string.Join("; ", results.Select(r => r.ErrorMessage));
        throw new ValidationException($"User validation failed: {errorMessages}");
    }
}