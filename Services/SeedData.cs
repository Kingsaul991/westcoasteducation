using WestcoastEducation.Models;
 
namespace WestcoastEducation.Services;
 
public static class SeedData

{
    public static void Populate(SchoolRegistry registry)

    {

        var today = DateOnly.FromDateTime(DateTime.Today);
        var anna = new Teacher

        {

            FirstName = "Anna", LastName = "Lind", PersonalIdentityNumber = "19800512-1234",

            PhoneNumber = "070-111 22 33", AreaOfExpertise = "C# and .NET",

            Address = new Address { Street = "Hamngatan 4", Postalcode = "442 30", City = "Kungälv" }

        };

        var erik = new Teacher

        {

            FirstName = "Erik", LastName = "Berg", PersonalIdentityNumber = "19751103-5678",

            PhoneNumber = "070-444 55 66", AreaOfExpertise = "Web development",

            Address = new Address { Street = "Strandvägen 9", Postalcode = "471 31", City = "Skärhamn" }

        };

        var maria = new EducationManager

        {

            FirstName = "Maria", LastName = "Holm", PersonalIdentityNumber = "19700220-2468",

            PhoneNumber = "031-12 34 56", AreaOfExpertise = "Mobile development",

            EmploymentDate = new DateOnly(2010, 8, 15),

            Address = new Address { Street = "Kyrkogatan 2", Postalcode = "442 31", City = "Kungälv" }

        };

        var lars = new Administrator

        {

            FirstName = "Lars", LastName = "Ek", PersonalIdentityNumber = "19650909-1357",

            PhoneNumber = "031-65 43 21", AreaOfExpertise = "Course administration",

            EmploymentDate = new DateOnly(2001, 1, 7),

            Address = new Address { Street = "Västra vägen 1", Postalcode = "442 34", City = "Kungälv" }

        };
 
        registry.AddStaff(anna);

        registry.AddStaff(erik);

        registry.AddStaff(maria);

        registry.AddStaff(lars);
 

        string[,] students =

        {

            { "Sara", "Nilsson", "20010101-1111", "Ytterby" },

            { "Omar", "Ali", "19990315-2222", "Kungälv" },

            { "Linnea", "Svensson", "20020707-3333", "Stenungsund" },

            { "Johan", "Karlsson", "19981212-4444", "Göteborg" },

            { "Fatima", "Hassan", "20000404-5555", "Kungälv" },

            { "Elias", "Persson", "19970606-6666", "Marstrand" }

        };
 
        for (var i = 0; i < students.GetLength(0); i++)

        {

            registry.Students.Add(new Student

            {

                FirstName = students[i, 0],

                LastName = students[i, 1],

                PersonalIdentityNumber = students[i, 2],

                PhoneNumber = $"073-000 00 0{i + 1}",

                Address = new Address { Street = $"Skolvägen {i + 1}", Postalcode = "442 40", City = students[i, 3] }

            });

        }
 
        var csharp = new ClassroomCourse

        {

            CourseNumber = "WCE-101", Title = "C# Fundamentals", Duration = 6, DurationUnit = DurationUnit.Weeks,

            StartDate = today.AddDays(45), EndDate = today.AddDays(45 + 6 * 7), Classroom = "Room 2A, Kungälv"

        };

        var web = new DistanceCourse

        {

            CourseNumber = "WCE-201", Title = "Modern Web with React", Duration = 4, DurationUnit = DurationUnit.Weeks,

            StartDate = today.AddDays(60), EndDate = today.AddDays(60 + 4 * 7), MeetingLink = "https://meet.westcoast.example/wce-201"

        };

        var mobile = new ClassroomCourse

        {

            CourseNumber = "WCE-301", Title = ".NET MAUI Workshop", Duration = 3, DurationUnit = DurationUnit.Days,

            StartDate = today.AddDays(30), EndDate = today.AddDays(32), Classroom = "Room 1B, Kungälv"

        };
 
        csharp.AssignTeacher(anna);

        web.AssignTeacher(erik);

        mobile.AssignTeacher(maria);
 
     

        var all = registry.Students.GetAll();

        for (var i = 0; i < all.Count; i++)

        {

            csharp.Enroll(all[i]);

            if (i % 2 == 0) web.Enroll(all[i]);

            if (i < 3) mobile.Enroll(all[i]);

        }
 
        registry.Courses.Add(csharp);

        registry.Courses.Add(web);

        registry.Courses.Add(mobile);

    }

}
 