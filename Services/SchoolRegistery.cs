using WestcoastEducation.Interfaces;

using WestcoastEducation.Models;
 
namespace WestcoastEducation.Services;
 

public class SchoolRegistry

{

    public IRepository<Course> Courses { get; } = new Repository<Course>();

    public IRepository<Student> Students { get; } = new Repository<Student>();

    public IRepository<Teacher> Teachers { get; } = new Repository<Teacher>();

    public IRepository<EducationManager> EducationManagers { get; } = new Repository<EducationManager>();

    public IRepository<Administrator> Administrators { get; } = new Repository<Administrator>();
 

    public List<Teacher> AllStaff()

    {

        var staff = new List<Teacher>();

        staff.AddRange(Teachers.GetAll());

        staff.AddRange(EducationManagers.GetAll());

        staff.AddRange(Administrators.GetAll());

        return staff;

    }
 
    public Teacher? FindStaff(string personalIdentityNumber)

    {

        foreach (var staff in AllStaff())

        {

            if (staff.Key == personalIdentityNumber)

            {

                return staff;

            }

        }
 
        return null;

    }
 

    public bool AddStaff(Teacher staff)

    {

        if (FindStaff(staff.Key) != null)

        {

            return false;

        }

        if (staff is Administrator administrator)

        {

            return Administrators.Add(administrator);

        }

        else if (staff is EducationManager manager)

        {

            return EducationManagers.Add(manager);

        }

        else

        {

            return Teachers.Add(staff);

        }

    }
 
    public bool RemoveCourse(string courseNumber)

    {

        var course = Courses.Find(courseNumber);

        if (course == null)

        {

            return false;

        }
 
        course.RemoveTeacher();

        return Courses.Remove(courseNumber);

    }

}
 