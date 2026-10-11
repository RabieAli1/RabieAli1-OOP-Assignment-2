namespace SrpLab.SrpLab.CheckoutBasket;

public sealed class PaymentGatewayProcessor
{
    public string GenerateAuthorizationStub(decimal grandTotal, string cardLast4, int itemCnt)
    {
        var payload = $"{grandTotal:0.00}|{cardLast4}|{itemCnt}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}