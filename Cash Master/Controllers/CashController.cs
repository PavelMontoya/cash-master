using Contracts;
using Entities;
using Resources.Const;
using Resources.Enums;

namespace Controllers;

public class CashController
{
    private readonly ICash _cash;
    private readonly IValidation _validation;

    public CashController(ICash cash, IValidation validation) => 
        (_cash, _validation) = (cash, validation);

    public MessageResponse GetChange(string totalCharge, string cashProvided)
    {
        var total = _cash.ConvertTotalCharge2Decimal(totalCharge);
        if (total == null)
        {
            return new MessageResponse(Texts.ERROR_INVALID_TC, Status.Failed);
        }

        var cash = _cash.ConvertCashProvided2Decimal(cashProvided);
        if (cash.Length == 0)
        {
            return new MessageResponse(Texts.ERROR_INVALID_CP, Status.Failed);
        }

        if (!_validation.ValidateCashProvidedIsEnough(total.Value, cash))
        {
            return new MessageResponse(Texts.ERROR_NOT_ENOUGH, Status.Failed);
        }
        
        var result = _cash.GetChange(total.Value, cash);
        return new MessageResponse(result, Status.Success);
    }
}
