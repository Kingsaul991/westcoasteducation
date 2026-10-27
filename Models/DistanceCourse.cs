using System.Text.Json.Serialization;
namespace WestcoastEducation.Models;
 
public class DistanceCourse : Course

{
    public string MeetingLink { get; set; } = "";
 
    [JsonIgnore]

    public override string DeliveryMode => "Distance";
 
    public override string ToString() => $"{base.ToString()}, link: {MeetingLink}";
}
 