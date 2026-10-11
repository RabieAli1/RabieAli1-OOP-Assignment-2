namespace SrpLab.SrpLab.CourseEnrollmentDesk;

public sealed class CourseWelcomePacketFormatter
{
    public string FormatWelcomePacket(string courseCode, string studentName, string status)
    {
        return $"# Welcome to {courseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{courseCode.ToLowerInvariant()}\n";
    }
}