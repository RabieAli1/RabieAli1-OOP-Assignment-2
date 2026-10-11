namespace SrpLab.SrpLab.Appointments;

public sealed class AppointmentReminderFormatter
{
    public string FormatSmsReminder(DateTimeOffset slot, string clinicPhone)
    {
        return $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
    }
}