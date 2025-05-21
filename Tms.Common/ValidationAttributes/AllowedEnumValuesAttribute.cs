using System.ComponentModel.DataAnnotations;

namespace Tms.Common.ValidationAttributes;

public class AllowedEnumValuesAttribute<TEnum> : ValidationAttribute where TEnum : Enum
{
    private readonly HashSet<string> _allowedValues = new(
        Enum.GetNames(typeof(TEnum)),
        StringComparer.OrdinalIgnoreCase
    );

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var memberName = validationContext.MemberName!;
        
        if (value == null)
            return new ValidationResult("Value is required.", [memberName]);

        var stringValue = value.ToString();

        if (stringValue == null || !_allowedValues.Contains(stringValue))
        {
            return new ValidationResult($"The value '{stringValue}' is not valid. Allowed values are: {string.Join(", ", _allowedValues)}.",
                [memberName]);
        }

        return ValidationResult.Success;
    }
}