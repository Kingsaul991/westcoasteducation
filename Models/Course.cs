using WestcoastEducation.Interfaces;
 
namespace WestcoastEducation.Models;
 
public abstract class Course : IIdentifiable
{
    public string CourseNumber { get; set; } = "";
    public string Title { get; set; } = "";
    public int Duration { get; set; }
    public DurationUnit DurationUnit { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
 

    public Teacher? Teacher { get; private set; }
    public List<Student> Students { get; private set; } = new();
 

    public string Key => CourseNumber;
 

    public abstract string DeliveryMode { get; }
 
    public void AssignTeacher(Teacher teacher)
    {
        Teacher?.RemoveCourse(this);
        Teacher = teacher;
        teacher.AddCourse(this); 
    }
    public void RemoveTeacher()
    {
        Teacher?.RemoveCourse(this);
        Teacher = null;
    }

    public bool Enroll(Student student)
    {
        foreach (var enrolled in Students)
        {
            if (enrolled.PersonalIdentityNumber == student.PersonalIdentityNumber)
            {
                return false;
            }
        }
 
        Students.Add(student);
        return true;
    }
 
    public override string ToString() =>
        $"{CourseNumber} - {Title} ({DeliveryMode}), {StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}, " +
        $"{Duration} {DurationUnit.ToString().ToLower()}, teacher: {Teacher?.FirstName ?? "not assigned"}, " +
        $"participants: {Students.Count}";
}