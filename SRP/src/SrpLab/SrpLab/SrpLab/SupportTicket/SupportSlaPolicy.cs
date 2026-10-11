namespace SrpLab.SrpLab.SupportTicket;

public sealed class SupportSlaPolicy
{
    public DateTimeOffset CalculateSlaDeadline(DateTimeOffset openedAt, string priority)
    {
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };
        return openedAt.AddHours(hours);
    }

    public bool IsBreached(DateTimeOffset deadline, DateTimeOffset now)
    {
        return now > deadline;
    }
}