using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Tms.Common.Exceptions;

public class ValidationException: Exception
{
    public override string Message { get; }

    private IList<ValidationDetails> Details { get; }

    public ValidationException(List<ValidationResult> results)
    {
        Details = results.Select(x => new ValidationDetails(x.ErrorMessage ?? string.Empty, x.MemberNames)).ToList();
        Message = JsonSerializer.Serialize(new {Errors = Details});
    }

    private record ValidationDetails(string Message, IEnumerable<string> Properties);
}