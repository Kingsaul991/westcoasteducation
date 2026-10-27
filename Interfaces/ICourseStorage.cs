using WestcoastEducation.Models;
 
namespace WestcoastEducation.Interfaces;
public interface ICourseStorage
{
    void Save(IEnumerable<Course> courses);
    List<Course> Load();
}