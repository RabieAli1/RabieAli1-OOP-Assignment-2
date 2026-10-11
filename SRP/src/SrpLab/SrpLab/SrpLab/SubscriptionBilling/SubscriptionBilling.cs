namespace SrpLab.SrpLab.SubscriptionBilling;

public sealed class SubscriptionBilling
{
    private readonly InvoiceSequenceGenerator _sequenceGenerator = new();
    private readonly DunningEmailFormatter _dunningFormatter = new();
    private readonly SubscriptionLedgerExporter _ledgerExporter = new();

    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        if (activeFrom <= PeriodStart) return MonthlyPrice;
        if (activeFrom >= PeriodEnd) return 0m;
        var totalDays = PeriodEnd.DayNumber - PeriodStart.DayNumber;
        if (totalDays <= 0) return MonthlyPrice;
        var used = PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(MonthlyPrice * used / totalDays, 2);
    }

    public string NextInvoiceNumber()
    {
        return _sequenceGenerator.MintNextInvoiceNumber(PeriodStart);
    }

    public void RegisterFailedPayment() => FailedPayments++;

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();
        return _dunningFormatter.FormatDunningEmail(customerName, amount, asOf, FailedPayments, invoice);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        var invoice = NextInvoiceNumber();
        var amount = Prorate(activeFrom);
        return _ledgerExporter.FormatLedgerJournalLine(CustomerId, invoice, amount);
    }
}