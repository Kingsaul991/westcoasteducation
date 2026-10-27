using System.Text.Json.Serialization;
 
namespace WestcoastEducation.Models;
 
[JsonConverter(typeof(JsonStringEnumConverter<DurationUnit>))]

public enum DurationUnit

{
    Days,

    Weeks

} 