public class Staff : Person
{
    private readonly decimal _allowance;

    public DateTime HireDate { get; }
    public decimal MonthlySalary { get; private set; }

    protected Staff(string personId, string fullName, string phone,
                    DateTime hireDate, decimal monthlySalary, decimal allowance)
        : base(personId, fullName, phone)
    {
        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        _allowance = allowance;
    }

    public void GiveRaise(decimal percent)
    {
        if (percent <= 0)
            throw new ArgumentException("Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * percent / 100;
    }

    public decimal GetMonthlyPay()
    {
        return MonthlySalary + _allowance;
    }
}
