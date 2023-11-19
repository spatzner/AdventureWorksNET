using System.Text.RegularExpressions;

namespace AdventureWorks.Domain.Person;

internal static partial class CompiledRegex
{
    [GeneratedRegex(@"(\d{1})(\d{2})(\d{3})(\d{3})(\d{4})")]
    public static partial Regex InternationalPhoneFormatGrouping();

    [GeneratedRegex(@"(\d{3})(\d{3})(\d{4})")]
    public static partial Regex NationalPhoneFormatGrouping();

    [GeneratedRegex(@"\D")]
    public static partial Regex NonDigit();
}