using System.Text;

using WestcoastEducation.Services;

using WestcoastEducation.UI;
 
Console.OutputEncoding = Encoding.UTF8;
 
var registry = new SchoolRegistry();

SeedData.Populate(registry);
 
var storage = new JsonCourseStorage(Path.Combine("data", "courses.json"));

new ConsoleMenu(registry, storage).Run();