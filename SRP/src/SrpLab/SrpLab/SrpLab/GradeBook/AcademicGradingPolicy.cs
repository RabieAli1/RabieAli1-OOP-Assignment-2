namespace SrpLab.SrpLab.GradeBook;

public sealed class AcademicGradingPolicy
{
    public string CalculateLetter(decimal average)
    {
        if (average >= 90) return "A";
        if (average >= 80) return "B";
        if (average >= 70) return "C";
        if (average >= 60) return "D";
        return "F";
    }

    public bool MeetsHonorRoll(decimal average, string letterGrade)
    {
        return average >= 85 && letterGrade is "A" or "B";
    }
}