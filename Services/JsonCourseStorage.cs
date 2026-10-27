using System.Text.Encodings.Web;

using System.Text.Json;

using WestcoastEducation.Interfaces;

using WestcoastEducation.Models;
 
namespace WestcoastEducation.Services;
 
public class JsonCourseStorage : ICourseStorage

{

    private static readonly JsonSerializerOptions Options = new()

    {

        WriteIndented = true,

        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

    };
 
    private readonly string _filePath;
 
    public JsonCourseStorage(string filePath)

    {

        _filePath = filePath;

    }
 
    public void Save(IEnumerable<Course> courses)

    {

        var directory = Path.GetDirectoryName(_filePath);

        if (!string.IsNullOrEmpty(directory))

        {

            Directory.CreateDirectory(directory);

        }
 
        var json = JsonSerializer.Serialize(courses.ToList(), Options);

        File.WriteAllText(_filePath, json);

    }

    public List<Course> Load()

    {

        if (!File.Exists(_filePath))

        {

            return new List<Course>();

        }
 
        var json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Course>>(json, Options) ?? new List<Course>();

    }

}
 