namespace SrpLab.SrpLab.CheckoutBasket;

public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;

    private readonly CouponEvaluator _couponEvaluator = new();
    private readonly GiftCardFormatter _giftCardFormatter = new();
    private readonly PaymentGatewayProcessor _paymentProcessor = new();

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);

    public decimal DiscountAmount() => _couponEvaluator.CalculateDiscount(_couponRaw, SubTotal());

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        if (_giftWrap) total += 4.99m;
        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        return _giftCardFormatter.FormatCard(fromName, _lines.Select(l => l.Sku), GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentProcessor.GenerateAuthorizationStub(GrandTotal(), cardLast4, _lines.Count);
    }
}