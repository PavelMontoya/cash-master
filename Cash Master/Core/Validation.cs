using Contracts;

namespace Core;

public class Validation : IValidation
{
    public bool ValidateCashProvidedIsEnough(decimal totalCharge, decimal[] cashProvided)
    {
        return cashProvided.Sum() >= totalCharge;
    }
}
