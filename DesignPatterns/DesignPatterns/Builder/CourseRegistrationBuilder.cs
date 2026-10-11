namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistrationBuilder
{
    private string _studentEmail = string.Empty;
    private string _courseCode = string.Empty;
    private string _accessMode = "VideosOnly";
    private string? _groupCode;
    private string? _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome = true;
    private string? _mentorNote;
    private DateOnly? _preferredStart;

    public CourseRegistrationBuilder ForStudent(string studentEmail)
    {
        _studentEmail = studentEmail;
        return this;
    }

    public CourseRegistrationBuilder ForCourse(string courseCode)
    {
        _courseCode = courseCode;
        return this;
    }

    public CourseRegistrationBuilder AsLiveGroup(string groupCode)
    {
        _accessMode = "LiveGroup";
        _groupCode = groupCode;
        return this;
    }

    public CourseRegistrationBuilder AsVideosOnly()
    {
        _accessMode = "VideosOnly";
        _groupCode = null;
        return this;
    }

    public CourseRegistrationBuilder WithDiscount(string discountCode)
    {
        _discountCode = discountCode;
        return this;
    }

    public CourseRegistrationBuilder SendWhatsApp(bool send = true)
    {
        _sendWhatsApp = send;
        return this;
    }

    public CourseRegistrationBuilder SendEmailWelcome(bool send = true)
    {
        _sendEmailWelcome = send;
        return this;
    }

    public CourseRegistrationBuilder WithMentorNote(string note)
    {
        _mentorNote = note;
        return this;
    }

    public CourseRegistrationBuilder WithPreferredStart(DateOnly startDate)
    {
        _preferredStart = startDate;
        return this;
    }

    public CourseRegistration Build()
    {
        // R3: Handled directly by CourseRegistration constructor during instantiating
        return new CourseRegistration(
            _studentEmail,
            _courseCode,
            _accessMode,
            _groupCode,
            _discountCode,
            _sendWhatsApp,
            _sendEmailWelcome,
            _mentorNote,
            _preferredStart
        );
    }
}