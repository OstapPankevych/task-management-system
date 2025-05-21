namespace Tms.Common.Extensions;

public static class EnumExtension
{
    public static TEnum? GetNext<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var values = Enum.GetValues<TEnum>().ToList();
        var index = values.IndexOf(value);
        var isLastIndex = values.Count == ++index;
        if (isLastIndex)
        {
            return null;
        }

        return values[index];
    }
}