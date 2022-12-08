namespace Contracts;

public interface IValidation
{
    bool ValidateCashProvidedIsEnough(decimal totalCharge, decimal[] cashProvided);
}
