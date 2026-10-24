namespace WestcoastEducation.Models;

public class EducationManager : Teacher
{
public DateOnly EmploymentDate { get; set; }
 
    public override string ToString() => $"{base.ToString()}, employed since {EmploymentDate:yyyy-MM-dd}";
}