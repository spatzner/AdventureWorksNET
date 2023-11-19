namespace AdventureWorks.Domain.Person.Entities;

public readonly struct PhoneNumber
{
    public string Number { get; }
    public string Type { get; }

    public PhoneNumber(string phoneNumber, string type)
    {
        if (!TryParse(phoneNumber, out string formattedNumber))
            throw new ArgumentException("Invalid length. Must be 10 or 13 digits in any format",
                nameof(phoneNumber));

        Number = formattedNumber;
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
                result = CompiledRegex.NationalPhoneFormatGrouping().Replace(numericOnly, "$1-$2-$3");
                return true;
            case 13:
                result = CompiledRegex.InternationalPhoneFormatGrouping().Replace(numericOnly, "$1 ($2) $3 $4-$5");
                return true;
            default:
                result = string.Empty;
                return false;
        }
    }

}