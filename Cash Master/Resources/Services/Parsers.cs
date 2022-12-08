namespace Resources;

public static class Parsers
{
    public static decimal? ParseDecimal(string value)
    {
        var trimmedValue = value.Trim(new char[] { '$', ',' });

        var successParse = decimal.TryParse(trimmedValue, out var parseResult);

        return successParse ? parseResult : null;
    }

    public static decimal[] ParseListOfDecimals(string value)
    {
        var splittedValue = value.Split(',', StringSplitOptions.TrimEntries);
        decimal[] parsedValue = Array.ConvertAll<string, decimal>(splittedValue, Convert.ToDecimal);

        return parsedValue;
    }
}
