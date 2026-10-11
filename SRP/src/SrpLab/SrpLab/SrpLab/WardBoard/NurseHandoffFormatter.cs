namespace SrpLab.SrpLab.WardBoard;

public sealed class NurseHandoffFormatter
{
    public string BuildHandoffNote(int bed, string? patient, int acuity)
    {
        if (string.IsNullOrEmpty(patient))
            return $"Bed {bed}: empty";

        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}