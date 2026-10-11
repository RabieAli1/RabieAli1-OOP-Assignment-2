public class Member : Person
{
    private readonly List<Loan> _loans = new List<Loan>();

    public int MaxLoans { get; }
    public decimal DiscountPercent { get; }
    public IReadOnlyList<Loan> Loans => _loans;

    protected Member(string personId, string fullName, string phone, int maxLoans, decimal discountPercent)
        : base(personId, fullName, phone)
    {
        MaxLoans = maxLoans;
        DiscountPercent = discountPercent;
    }

    public int ActiveLoansCount()
    {
        int count = 0;
        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                count++;
        }
        return count;
    }

    public Loan Borrow(string loanId, LibraryItem item, DateTime borrowDate)
    {
        if (ActiveLoansCount() >= MaxLoans)
            throw new InvalidOperationException(
                $"{FullName} already has the maximum of {MaxLoans} active loans.");

        Loan loan = new Loan(loanId, this, item, borrowDate);
        _loans.Add(loan);
        return loan;
    }
}
