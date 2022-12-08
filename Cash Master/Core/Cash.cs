using System.Globalization;
using Contracts;
using Resources.Const;

namespace Core;

public class Cash : ICash
{
    public decimal? ConvertTotalCharge2Decimal(string chargeTotal)
    {
        return ParseString2Decimal(chargeTotal);
    }

    public decimal[] ConvertCashProvided2Decimal(string cashProvided)
    {
        var splitCashProvided = cashProvided.Split(",", StringSplitOptions.TrimEntries);
        
        decimal[] results = new decimal[splitCashProvided.Length];
        
        for (int i = 0; i < splitCashProvided.Length; i++)
        {
            var result = ParseString2Decimal(splitCashProvided[i]);
            
            if (result == null)
            {
                return Array.Empty<decimal>();
            }

            results[i] = result.Value;
        }

        return results;
    }
    
    public string GetChange(decimal totalCharge, decimal[] cashProvided)
    {
        var changeNeeded = cashProvided.Sum() - totalCharge;
        if (changeNeeded == 0)
        {
            return changeNeeded.ToString(CultureInfo.CurrentCulture);
        }

        var region = Environment.GetEnvironmentVariable("REGION");
        
        var billsAndCoins = Currency.BillsAndCoins[region];
        Array.Sort(billsAndCoins);
        Array.Reverse(billsAndCoins);

        string change = "";
        decimal changeSum = 0;
        
        foreach (var bill in billsAndCoins)
        {
            while (changeNeeded >= changeSum + bill)
            {
                change += bill.ToString(CultureInfo.CurrentCulture) + ", ";
                changeSum += bill;
            }
        }
        
        return change.Trim(new char[]{',', ' '});
    }

    private decimal? ParseString2Decimal(string value)
    {
        var formattedValue = value
            .Replace( ",", "")
            .Replace("$", "")
            .Trim();
        var parseSucceeded = decimal.TryParse(formattedValue, out var parseValue);
        if (parseSucceeded)
        {
            return parseValue;
        }

        return null;
    }
}