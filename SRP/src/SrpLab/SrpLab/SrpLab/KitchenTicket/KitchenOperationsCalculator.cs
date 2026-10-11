namespace SrpLab.SrpLab.KitchenTicket;

public sealed class KitchenOperationsCalculator
{
    public int CalculateEstimatedReadyMinutes(IReadOnlyList<int> prepTimes, bool hasAllergens, int openStations)
    {
        if (openStations <= 0) openStations = 1;
        var sequential = prepTimes.Sum();
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (hasAllergens) parallel += 3;
        var longest = prepTimes.Count == 0 ? 0 : prepTimes.Max();
        return Math.Max(parallel, longest);
    }

    public string DetermineExpoLaneHint(bool hasAllergens, int estimatedMinutes)
    {
        return hasAllergens ? "LANE-ALLERGY" : estimatedMinutes > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}