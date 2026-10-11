namespace SrpLab.SrpLab.WardBoard;

public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();
    private readonly Dictionary<int, int> _vitalsScore = new();

    private readonly ClinicalAcuityCalculator _acuityCalculator = new();
    private readonly EmergencyPagerSystem _pagerSystem = new();
    private readonly NurseHandoffFormatter _handoffFormatter = new();
    private readonly WardCensusExporter _censusExporter = new();

    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");

        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        var acuity = ScoreAcuity(heartRate, spo2);
        _vitalsScore[bed] = acuity;

        _pagerSystem.RecordAlertIfCritical(bed, acuity);
    }

    public int ScoreAcuity(int heartRate, int spo2)
    {
        return _acuityCalculator.ScoreAcuity(heartRate, spo2);
    }

    public string BuildHandoffNote(int bed)
    {
        _bedPatient.TryGetValue(bed, out var patient);
        var acuity = _vitalsScore.TryGetValue(bed, out var score) ? score : 0;
        return _handoffFormatter.BuildHandoffNote(bed, patient, acuity);
    }

    public IReadOnlyList<string> DrainPagerLog()
    {
        return _pagerSystem.DrainPagerLog();
    }

    public string ExportCensusCsv()
    {
        return _censusExporter.ExportCensusCsv(_bedPatient, _vitalsScore);
    }
}