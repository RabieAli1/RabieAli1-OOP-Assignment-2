namespace SrpLab.SrpLab.SupportTicket;

public sealed class SupportMessageFormatter
{
    public string DraftPublicReply(string ticketId, string priority, DateTimeOffset slaDeadline, string agentName)
    {
        var apology = priority == "P1" ? "We are treating this as a critical incident." : "Thanks for reaching out.";
        return $"Hi,\n{apology}\nTicket {ticketId} is with {agentName}. Next update before {slaDeadline:u}.\n";
    }

    public string FormatInternalEscalationBlurb(string ticketId, string priority, DateTimeOffset slaDeadline)
    {
        return $"ESCALATE {ticketId} priority={priority} breachAt={slaDeadline:u} keywords-scanned=yes";
    }
}