namespace WestcoastEducation.Models;
 
public class ClassroomCourse : Course
{
    public string Classroom { get; set; } = "";
 
    public override string DeliveryMode => "Classroom";
 
    public override string ToString() => $"{base.ToString()}, room: {Classroom}";
}
