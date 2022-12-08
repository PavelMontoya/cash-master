namespace CashTests;

public class Tests
{
    private Cash cash;
    private Validation validation;

    [SetUp]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("REGION", "MX");
        cash = new Cash();
        validation = new Validation();
    }

    [Test]
    public void Just_pass()
    {
        Assert.Pass();
    }

    [Test]
    public void NotSufficientProvidedCashValidation()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                validation.ValidateCashProvidedIsEnough(150.10m, new[] { 100m, 50m }),
                Is.EqualTo(false));
            Assert.That(
                validation.ValidateCashProvidedIsEnough(150.10m, new[] { 100.00m, 100.00m }),
                Is.EqualTo(true));
        });
    }

    [Test]
    public void ConvertTotalCharge2DecimalSuccess()
    {
        Assert.That(
            cash.ConvertTotalCharge2Decimal("$66.6"),
            Is.EqualTo(66.6m)
        );
    }

    [Test]
    public void ConvertTotalCharge2DecimalFail()
    {
        Assert.That(
            cash.ConvertTotalCharge2Decimal("Error"),
            Is.Null
        );
    }

    [Test]
    public void GetChangeNoChangeNeeded()
    {
        Assert.That(
            cash.GetChange(151.00m, new[] { 100.00m, 50.00m, 1.00m }),
            Is.EqualTo("0.00")
        );
    }
    
    [Test]
    public void GetChangeNoChange()
    {
        Assert.That(
            cash.GetChange(150.90m, new[] { 100.00m, 100.00m }),
            Is.EqualTo("20.00, 20.00, 5.00, 2.00, 2.00, 0.10")
        );
    }
}