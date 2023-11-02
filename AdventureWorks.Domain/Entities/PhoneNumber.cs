using System.Text.RegularExpressions;

namespace AdventureWorks.Domain.Entities;

public readonly struct PhoneNumber
{
    public string Number { get; }
    public string Type { get; }

    public PhoneNumber(string phoneNumber, string type)
    {
        var numericOnly = Regex.Replace(@"\D", phoneNumber, string.Empty);

        switch (numericOnly.Length)
        {
            case 10:
                Number = Regex.Replace(@"(\d{3})(\d{3})(\d{4})", numericOnly, "$1-$2-$3");
                ;
                break;
            case 13:
                Number = Regex.Replace(@"(\d{1})(\d{2})(\d{3})(\d{3})(\d{4})", numericOnly, "$1 ($2) $3 $4-$5");
                break;
            default:
                throw new ArgumentException("Invalid length. Must be 10 or 13 digits in any format",
                    nameof(phoneNumber));
        }

        Type = type;
    }

}