namespace SrpLab.SrpLab.WardBoard;

public sealed class WardCensusExporter
{
    public string ExportCensusCsv(IDictionary<int, string> bedPatient, IDictionary<int, int> vitalsScore)
    {
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in bedPatient.Keys.OrderBy(x => x))
        {
            lines.Add($"{bed},{bedPatient[bed]},{vitalsScore[bed]}");
        }
        return string.Join('\n', lines);
    }
}