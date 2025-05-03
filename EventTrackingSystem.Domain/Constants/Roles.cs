namespace EventTrackingSystem.Domain.Constants;

public static class Roles
{
    public const string Admin = "Адміністратор";
    public const string Student = "Студент";
    public const string Teacher = "Викладач";
    public const string Guest = "Гість";

    public static readonly IReadOnlyList<string> All = [Admin, Student, Teacher, Guest];
}
