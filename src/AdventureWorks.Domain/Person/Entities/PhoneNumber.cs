namespace AdventureWorks.Domain.Person.Entities;

public class PhoneNumber : IValidatable
{
    public string Number
    {
        get => _number;
        set
        {
            if (!TryParse(value, out string formattedNumber))
                throw new ArgumentException("Invalid length. Must be 10 or 13 digits in any format", nameof(Number));

            _number = formattedNumber;
        }
    }

    public string Type { get; }
    private string _number = null!;

    public PhoneNumber(string phoneNumber, string type)
    {
        Number = phoneNumber;
        Type = type;
    }

    public static bool TryParse(string? phoneNumber, out string result)
    {
        if (phoneNumber == null)
        {
            result = string.Empty;
            return false;
        }

        string numericOnly = CompiledRegex.NonDigit().Replace(phoneNumber, string.Empty);

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