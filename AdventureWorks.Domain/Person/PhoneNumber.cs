using System.Text.RegularExpressions;

namespace AdventureWorks.Domain.Person;

public readonly struct PhoneNumber
{
    public string Number { get; }
    public string Type { get; }

    public PhoneNumber(string phoneNumber, string type)
    {
        if(!TryParse(phoneNumber, out string result))
            throw new ArgumentException("Invalid length. Must be 10 or 13 digits in any format",
                nameof(phoneNumber));
        
        Number = result;
        Type = type;
    }

    public static bool TryParse(string? phoneNumber, out string result)
    {
        if (phoneNumber == null)
        {
            result = string.Empty;
            return false;
        }

        var numericOnly = CompiledRegex.NonDigit().Replace(phoneNumber, string.Empty);

        switch (numericOnly.Length)
        {
            case 10:
                result = CompiledRegex.NationalPhoneGroupingFormat().Replace(numericOnly, "$1-$2-$3");
                return true;
            case 13:
                result = CompiledRegex.InternationalPhoneGroupingFormat().Replace(numericOnly, "$1 ($2) $3 $4-$5");
                return true;
            default:
                result = string.Empty;
                return false;
        }
    }

}

internal static partial class CompiledRegex
{
    [GeneratedRegex(@"(\d{1})(\d{2})(\d{3})(\d{3})(\d{4})")]
    public static partial Regex InternationalPhoneGroupingFormat();

    [GeneratedRegex(@"(\d{3})(\d{3})(\d{4})")]
    public static partial Regex NationalPhoneGroupingFormat();

    [GeneratedRegex(@"\D")]
    public static partial Regex NonDigit();
}