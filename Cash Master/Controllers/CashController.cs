using Contracts;
using Entities;
using Resources.Enums;

namespace Controllers;

public class CashController
{
    private readonly ICash _cash;
    private readonly IValidation _validation;

    public CashController(ICash cash, IValidation validation) => 
        (_cash, _validation) = (cash, validation);

    public MessageResponse GetChange(string chargeTotal, string cashProvided)
    {
        var total = _cash.ConvertTotalCharge2Decimal(chargeTotal);
        if (total == null)
        {
            return new MessageResponse("Invalid charge total", Status.Failed);
        }

        var cash = _cash.ConvertCashProvided2Decimal(cashProvided);
        if (cash.Length == 0)
        {
            return new MessageResponse("Invalid cash provided", Status.Failed);
        }

        if (!_validation.ValidateCashProvidedIsEnough(total.Value, cash))
        {
            return new MessageResponse("Cash Provided is not enough", Status.Failed);
        }
        
        var result = _cash.GetChange(total.Value, cash);
        return new MessageResponse(result, Status.Success);
    }
}
