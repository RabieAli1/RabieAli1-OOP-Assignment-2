namespace SrpLab.SrpLab.SubscriptionBilling;

public sealed class SubscriptionLedgerExporter
{
    public string FormatLedgerJournalLine(string customerId, string invoiceNumber, decimal proratedAmount)
    {
        return $"{customerId},{invoiceNumber},{proratedAmount:0.00},AR-SUB";
    }
}