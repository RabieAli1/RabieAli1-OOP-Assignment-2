namespace SrpLab.SrpLab.LoanDesk;

public sealed class LoanDesk
{
    private readonly LoanUnderwritingEvaluator _evaluator = new();
    private readonly LoanDocumentCompliance _compliance = new();
    private readonly LoanDecisionLetterFormatter _letterFormatter = new();

    public decimal RequestedAmount { get; }
    public int CreditScore { get; }
    public int EmploymentMonths { get; }
    public bool HasCollateral { get; }

    public LoanDesk(decimal requestedAmount, int creditScore, int employmentMonths, bool hasCollateral)
    {
        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        EmploymentMonths = employmentMonths;
        HasCollateral = hasCollateral;
    }

    public decimal RiskScore()
    {
        return _evaluator.CalculateRiskScore(RequestedAmount, CreditScore, EmploymentMonths, HasCollateral);
    }

    public bool IsEligible()
    {
        return _evaluator.IsEligible(RiskScore(), CreditScore);
    }

    public IReadOnlyList<string> RequiredDocuments()
    {
        return _compliance.DetermineRequiredDocuments(RequestedAmount, EmploymentMonths, HasCollateral, IsEligible());
    }

    public string DecisionLetter(string applicantName)
    {
        return _letterFormatter.FormatDecisionLetter(applicantName, RequestedAmount, RiskScore(), IsEligible(), RequiredDocuments());
    }

    public string UnderwriterCsvRow(string applicationId)
    {
        return $"{applicationId},{CreditScore},{EmploymentMonths},{(HasCollateral ? 1 : 0)},{RiskScore():0.00},{(IsEligible() ? "Y" : "N")}";
    }
}