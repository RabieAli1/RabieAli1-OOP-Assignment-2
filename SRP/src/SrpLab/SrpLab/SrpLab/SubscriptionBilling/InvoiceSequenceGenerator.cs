namespace SrpLab.SrpLab.SubscriptionBilling;

public sealed class InvoiceSequenceGenerator
{
    private static int _invoiceSeq = 1000;

    public string MintNextInvoiceNumber(DateOnly periodStart)
    {
        var n = Interlocked.Increment(ref _invoiceSeq);
        return $"INV-{periodStart:yyyyMM}-{n:D5}";
    }
}