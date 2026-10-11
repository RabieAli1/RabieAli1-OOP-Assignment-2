public class Loan
{
    public string LoanId { get; }
    public DateTime BorrowDate { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }
    public LoanStatus Status { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public DateTime DueDate => BorrowDate.AddDays(Item.LoanPeriodDays);

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate == null)
                return 0;

            int daysLate = (ReturnDate.Value.Date - DueDate.Date).Days;
            if (daysLate <= 0)
                return 0;

            decimal fee = daysLate * Item.GetDailyLateFee();
            return fee - fee * Member.DiscountPercent / 100;
        }
    }

    internal Loan(string loanId, Member member, LibraryItem item, DateTime borrowDate)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID must not be empty.");
        if (member == null)
            throw new ArgumentException("A loan must have a member.");
        if (item == null)
            throw new ArgumentException("A loan must have an item.");

        item.MarkAsBorrowed();

        LoanId = loanId;
        Member = member;
        Item = item;
        BorrowDate = borrowDate;
        Status = LoanStatus.Borrowed;
    }

    internal void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException($"Loan {LoanId} cannot be returned: its status is {Status}.");
        if (returnDate < BorrowDate)
            throw new ArgumentException("Return date cannot be earlier than the borrow date.");

        Status = LoanStatus.Returned;
        ReturnDate = returnDate;
        Item.MarkAsReturned();
    }

    internal void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException($"Loan {LoanId} cannot be marked as lost: its status is {Status}.");

        Status = LoanStatus.Lost;
    }
}
