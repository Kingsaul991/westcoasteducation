namespace WestcoastEducation.Models;
 
public class Teacher : Person

{


    private readonly List<Course> _courses = new();
 
    public string AreaOfExpertise { get; set; } = "";
 
    public IReadOnlyList<Course> Courses => _courses;
 

    internal void AddCourse(Course course)

    {

        if (!_courses.Contains(course))

        {

            _courses.Add(course);

        }

    }
 
    internal void RemoveCourse(Course course) => _courses.Remove(course);
 
    public override string ToString()

    {

        var numbers = new List<string>();

        foreach (var course in _courses)

        {

            numbers.Add(course.CourseNumber);

        }
 
        var courseText = numbers.Count > 0 ? string.Join(", ", numbers) : "none";

        return $"{base.ToString()}, expertise: {AreaOfExpertise}, courses: {courseText}";

    }

}
 
