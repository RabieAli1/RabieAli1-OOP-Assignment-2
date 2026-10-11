namespace SrpLab.SrpLab.GradeBook;

public sealed class GradeExporter
{
    public string FormatTranscript(string studentId, string fullName, decimal average, string letter, bool isHonor)
    {
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {average}\nLetter: {letter}\nHonor: {isHonor}\n";
    }

    public string ExportCsv(IEnumerable<(string StudentId, decimal Average, string Letter, bool IsHonor)> records)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var r in records.OrderBy(x => x.StudentId, StringComparer.OrdinalIgnoreCase))
        {
            rows.Add($"{r.StudentId},{r.Average},{r.Letter},{(r.IsHonor ? 1 : 0)}");
        }
        return string.Join('\n', rows);
    }
}