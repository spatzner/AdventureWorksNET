using System.Text.RegularExpressions;

namespace AdventureWorks.Domain.Entities;

public readonly struct PhoneNumber
{
    public string Number { get; }
    public string Type { get; }

    public PhoneNumber(string phoneNumber, string type)
    {
        var numericOnly = CompiledRegex.NonDigit().Replace(phoneNumber, string.Empty);

        switch (numericOnly.Length)
        {
            case 10:
                Number = CompiledRegex.NationalPhoneGroupingFormat().Replace(numericOnly, "$1-$2-$3");
                ;
                break;
            case 13:
                Number = CompiledRegex.InternationalPhoneGroupingFormat().Replace(numericOnly, "$1 ($2) $3 $4-$5");
                break;
            default:
                throw new ArgumentException("Invalid length. Must be 10 or 13 digits in any format",
                    nameof(phoneNumber));
        }

        Type = type;
    }

}

public static partial class CompiledRegex
{
    [GeneratedRegex(@"(\d{1})(\d{2})(\d{3})(\d{3})(\d{4})")]
    public static partial Regex InternationalPhoneGroupingFormat();

    [GeneratedRegex(@"(\d{3})(\d{3})(\d{4})")]
    public static partial Regex NationalPhoneGroupingFormat();

    [GeneratedRegex(@"\D")]
    public static partial Regex NonDigit();
}