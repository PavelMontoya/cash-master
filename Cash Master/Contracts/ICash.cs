namespace Contracts;

public interface ICash
{
    public decimal? ConvertTotalCharge2Decimal(string totalCharge);
    public decimal[] ConvertCashProvided2Decimal(string cashProvided);
    public string GetChange(decimal totalCharge, decimal[] cashProvided);
}