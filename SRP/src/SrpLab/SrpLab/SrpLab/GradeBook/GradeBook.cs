namespace SrpLab.SrpLab.GradeBook;

public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);

    private readonly AcademicGradingPolicy _gradingPolicy = new();
    private readonly GradeExporter _gradeExporter = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) || list.Count == 0) return 0m;
        return Math.Round(list.Average(), 2);
    }

    public string Letter(string studentId)
    {
        return _gradingPolicy.CalculateLetter(Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        return _gradingPolicy.MeetsHonorRoll(Average(studentId), Letter(studentId));
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        return _gradeExporter.FormatTranscript(studentId, fullName, Average(studentId), Letter(studentId), MeetsHonorRoll(studentId));
    }

    public string ExportCsv()
    {
        var records = _scores.Keys.Select(id => (
            StudentId: id,
            Average: Average(id),
            Letter: Letter(id),
            IsHonor: MeetsHonorRoll(id)
        ));

        return _gradeExporter.ExportCsv(records);
    }
}