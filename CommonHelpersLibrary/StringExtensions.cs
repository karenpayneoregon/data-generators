using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonHelpersLibrary;
public static class StringExtensions
{
    public static string MaskSsn(this string ssn, int digitsToShow = 4, char maskCharacter = 'X')
    {
        if (string.IsNullOrWhiteSpace(ssn)) return string.Empty;
        if (ssn.Contains("-")) ssn = ssn.Replace("-", string.Empty);
        if (ssn.Length != 9) throw new ArgumentException("SSN invalid length");
        if (ssn.IsNotInteger()) throw new ArgumentException("SSN not valid");

        const int ssnLength = 9;
        const string separator = "-";
        int maskLength = ssnLength - digitsToShow;

        int output = int.Parse(ssn.Replace(separator, string.Empty).Substring(maskLength, digitsToShow));

        string format = string.Empty;
        for (int index = 0; index < maskLength; index++) format += maskCharacter;
        for (int index = 0; index < digitsToShow; index++) format += "0";

        format = format.Insert(3, separator).Insert(6, separator);
        format = $"{{0:{format}}}";

        return string.Format(format, output);
    }

    public static bool IsInteger(this string sender)
    {
        foreach (var c in sender)
            if (c is < '0' or > '9') return false;

        return true;
    }

    public static bool IsNotInteger(this string sender)
        => sender.IsInteger() == false;

}
