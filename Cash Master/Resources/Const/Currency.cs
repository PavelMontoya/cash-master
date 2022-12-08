namespace Resources.Const;

// For a lack of a better option I chose a Dictionary
public static class Currency
{
    public static Dictionary<string, decimal[]> BillsAndCoins = new Dictionary<string, decimal[]>()
    {
        { "US", new decimal[] { 0.01m, 0.05m, 0.10m, 0.25m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m }},
        { "MX", new decimal[] { 0.05m, 0.10m, 0.20m, 0.50m, 1.00m, 2.00m, 5.00m, 10.00m, 20.00m, 50.00m, 100.00m }}
    };
}
