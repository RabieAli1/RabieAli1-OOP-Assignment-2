public class LibraryItem
{
    private readonly decimal _lateFeeMultiplier;

    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }
    public decimal BaseLateFee { get; private set; }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; internal set; }

    protected LibraryItem(string catalogNumber, string title, decimal baseLateFee,
                          int loanPeriodDays, decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number must not be empty.");
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title must not be empty.");
        if (baseLateFee <= 0)
            throw new ArgumentException("Base late fee must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriodDays = loanPeriodDays;
        _lateFeeMultiplier = lateFeeMultiplier;
    }

    public decimal GetDailyLateFee()
    {
        return BaseLateFee * _lateFeeMultiplier;
    }

    internal void ChangeBaseLateFee(decimal newBaseLateFee)
    {
        if (newBaseLateFee <= 0)
            throw new ArgumentException("Late fee must be greater than zero.");

        BaseLateFee = newBaseLateFee;
    }

    internal void Withdraw()
    {
        IsWithdrawn = true;
    }

    internal void Restore()
    {
        IsWithdrawn = false;
    }

    internal void MarkAsBorrowed()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException($"'{Title}' is withdrawn and cannot be borrowed.");
        if (IsOnLoan)
            throw new InvalidOperationException($"'{Title}' is already on loan.");

        IsOnLoan = true;
    }

    internal void MarkAsReturned()
    {
        IsOnLoan = false;
    }
}
