namespace SrpLab.SrpLab.SupportTicket;

public sealed class SupportTicket
{
    private readonly TicketPriorityAnalyzer _priorityAnalyzer = new();
    private readonly SupportSlaPolicy _slaPolicy = new();
    private readonly SupportMessageFormatter _messageFormatter = new();

    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt)
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;
        RecalculatePriorityFromText();
    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
        RecalculatePriorityFromText();
    }

    public void RecalculatePriorityFromText()
    {
        Priority = _priorityAnalyzer.DeterminePriority(Subject, Body);
    }

    public DateTimeOffset SlaDeadline()
    {
        return _slaPolicy.CalculateSlaDeadline(OpenedAt, Priority);
    }

    public bool IsBreached(DateTimeOffset now)
    {
        return _slaPolicy.IsBreached(SlaDeadline(), now);
    }

    public string DraftPublicReply(string agentName)
    {
        return _messageFormatter.DraftPublicReply(Id, Priority, SlaDeadline(), agentName);
    }

    public string InternalEscalationBlurb()
    {
        return _messageFormatter.FormatInternalEscalationBlurb(Id, Priority, SlaDeadline());
    }
}