using System.ComponentModel.DataAnnotations;

namespace Tms.Common.Utils;

public static class DtoValidator
{
    public static void ThrowIfInvalid<T>(this T dto) where T: class
    {
        var context = new ValidationContext(dto, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        if (Validator.TryValidateObject(dto, context, results, validateAllProperties: true))
        {
            return;
        }
        
        throw new Exceptions.ValidationException(results);
    }
}