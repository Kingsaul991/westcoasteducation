using WestcoastEducation.Interfaces;

using WestcoastEducation.Models;

using WestcoastEducation.Services;
 
namespace WestcoastEducation.UI;
 
public class ConsoleMenu

{

    private readonly SchoolRegistry _registry;

    private readonly ICourseStorage _storage;
 
    public ConsoleMenu(SchoolRegistry registry, ICourseStorage storage)

    {

        _registry = registry;

        _storage = storage;

    }
 
    public void Run()

    {

        var running = true;
 
        while (running)

        {

            PrintMenu();

            var choice = ConsoleInput.ReadText("Choice");

            Console.WriteLine();

            running = HandleChoice(choice);

            Console.WriteLine();

        }
 
        Console.WriteLine("Goodbye!");

    }
 
    private static void PrintMenu()

    {

        Console.WriteLine("===== Westcoast Education =====");

        Console.WriteLine(" 1. Add course");

        Console.WriteLine(" 2. Add teacher");

        Console.WriteLine(" 3. Add student");

        Console.WriteLine(" 4. Add education manager (supervisor)");

        Console.WriteLine(" 5. Add administrator");

        Console.WriteLine(" 6. Assign teacher to course");

        Console.WriteLine(" 7. Enroll student on course");

        Console.WriteLine(" 8. Remove course");

        Console.WriteLine(" 9. List courses");

        Console.WriteLine("10. List students");

        Console.WriteLine("11. List staff");

        Console.WriteLine("12. Save courses to JSON");

        Console.WriteLine("13. Load courses from JSON");

        Console.WriteLine(" 0. Exit");

    }
        private bool HandleChoice(string choice)

    {

        switch (choice)

        {

            case "1": AddCourse(); break;

            case "2": AddStaff(ReadTeacher<Teacher>()); break;

            case "3": AddStudent(); break;

            case "4": AddStaff(ReadManager<EducationManager>()); break;

            case "5": AddStaff(ReadManager<Administrator>()); break;

            case "6": AssignTeacher(); break;

            case "7": EnrollStudent(); break;

            case "8": RemoveCourse(); break;

            case "9": PrintAll("Courses", _registry.Courses.GetAll()); break;

            case "10": PrintAll("Students", _registry.Students.GetAll()); break;

            case "11": PrintAll("Staff", _registry.AllStaff()); break;

            case "12": SaveCourses(); break;

            case "13": LoadCourses(); break;

            case "0": return false;

            default: Console.WriteLine("Unknown choice, try again."); break;

        }
 
        return true;

    }
 
    private static T ReadPerson<T>() where T : Person, new()

    {

        return new T

        {

            FirstName = ConsoleInput.ReadText("First name"),

            LastName = ConsoleInput.ReadText("Last name"),

            PersonalIdentityNumber = ConsoleInput.ReadText("Personal identity number"),

            PhoneNumber = ConsoleInput.ReadText("Telephone number"),

            Address = new Address

            {

                Street = ConsoleInput.ReadText("Street address"),

                Postalcode = ConsoleInput.ReadText("Postal code"),

                City = ConsoleInput.ReadText("Town/City")

            }

        };

    }
 
    private static T ReadTeacher<T>() where T : Teacher, new()

    {

        var teacher = ReadPerson<T>();

        teacher.AreaOfExpertise = ConsoleInput.ReadText("Area of expertise");

        return teacher;

    }
 
    private static T ReadManager<T>() where T : EducationManager, new()

    {

        var manager = ReadTeacher<T>();

        manager.EmploymentDate = ConsoleInput.ReadDate("Date of employment");

        return manager;

    }
 
    private void AddStudent()

    {

        var student = ReadPerson<Student>();
 
        if (_registry.Students.Add(student))

        {

            Console.WriteLine($"Added {student}");

        }

        else

        {

            Console.WriteLine("A student with that personal identity number already exists.");

        }

    }
 
    private void AddStaff(Teacher staff)

    {

        if (_registry.AddStaff(staff))

        {

            Console.WriteLine($"Added {staff}");

        }

        else

        {

            Console.WriteLine("A staff member with that personal identity number already exists.");

        }

    }
 
    private void AddCourse()

    {

        var kind = ConsoleInput.ReadChoice("Classroom or distance course", 'c', 'd');
 
        Course course;

        if (kind == 'c')

        {

            course = new ClassroomCourse { Classroom = ConsoleInput.ReadText("Classroom") };

        }

        else

        {

            course = new DistanceCourse { MeetingLink = ConsoleInput.ReadText("Meeting link") };

        }
 
        course.CourseNumber = ConsoleInput.ReadText("Course number");

        if (_registry.Courses.Find(course.CourseNumber) != null)

        {

            Console.WriteLine("A course with that number already exists.");

            return;

        }
 
        course.Title = ConsoleInput.ReadText("Title");

        course.DurationUnit = ConsoleInput.ReadChoice("Duration in days or weeks", 'd', 'w') == 'd'

            ? DurationUnit.Days

            : DurationUnit.Weeks;

        course.Duration = ConsoleInput.ReadInt("Duration", 1, 365);

        course.StartDate = ConsoleInput.ReadDate("Start date");

        course.EndDate = ConsoleInput.ReadDate("End date", notBefore: course.StartDate);
 
        _registry.Courses.Add(course);

        Console.WriteLine($"Added {course}");

    }
 
    private void AssignTeacher()

    {

        var course = FindCourse();

        if (course == null) return;
 
        var teacher = _registry.FindStaff(ConsoleInput.ReadText("Teacher's personal identity number"));

        if (teacher == null)

        {

            Console.WriteLine("No staff member with that number.");

            return;

        }
 
        course.AssignTeacher(teacher);

        Console.WriteLine($"{teacher.FirstName} {teacher.LastName} is now responsible for {course.CourseNumber}.");

    }
 
    private void EnrollStudent()

    {

        var course = FindCourse();

        if (course == null) return;
 
        var student = _registry.Students.Find(ConsoleInput.ReadText("Student's personal identity number"));

        if (student == null)

        {

            Console.WriteLine("No student with that number.");

            return;

        }
 
        if (course.Enroll(student))

        {

            Console.WriteLine($"{student.FirstName} is enrolled on {course.CourseNumber}.");

        }

        else

        {

            Console.WriteLine($"{student.FirstName} is already enrolled on {course.CourseNumber}.");

        }

    }
 
    private void RemoveCourse()

    {

        var number = ConsoleInput.ReadText("Course number");
 
        if (_registry.RemoveCourse(number))

        {

            Console.WriteLine($"Removed course {number}.");

        }

        else

        {

            Console.WriteLine($"No course with number {number}.");

        }

    }
 
    private Course? FindCourse()

    {

        var number = ConsoleInput.ReadText("Course number");

        var course = _registry.Courses.Find(number);
 
        if (course == null)

        {

            Console.WriteLine($"No course with number {number}.");

        }
 
        return course;

    }

    private static void PrintAll<T>(string heading, IEnumerable<T> items)

    {

        Console.WriteLine($"--- {heading} ---");

        var count = 0;
 
        foreach (var item in items)

        {

            Console.WriteLine(item);

            count++;

        }
 
        if (count == 0)

        {

            Console.WriteLine("(none)");

        }

    }
 
    private void SaveCourses()

    {

        try

        {

            _storage.Save(_registry.Courses.GetAll());

            Console.WriteLine($"Saved {_registry.Courses.GetAll().Count} course(s).");

        }

        catch (IOException ex)

        {

            Console.WriteLine($"Could not save courses: {ex.Message}");

        }

    }
 
    private void LoadCourses()

    {

        try

        {

            var loaded = _storage.Load();

            if (loaded.Count == 0)

            {

                Console.WriteLine("No saved courses found.");

                return;

            }
 
            var count = _registry.ReplaceCourses(loaded);

            Console.WriteLine($"Loaded {count} course(s).");

        }

        catch (Exception ex) when (ex is IOException or System.Text.Json.JsonException)

        {

            Console.WriteLine($"Could not load courses: {ex.Message}");

        }

    }

}
