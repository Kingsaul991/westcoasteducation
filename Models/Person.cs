using WestcoastEducation.Interfaces;
 
namespace WestcoastEducation.Models;
 
public abstract class Person : IIdentifiable

{

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string PersonalIdentityNumber { get; set; } = "";
 

    public Address Address { get; set; } = new();
 

    public string Key => PersonalIdentityNumber;
 
    public override string ToString() =>

        $"{FirstName} {LastName} ({PersonalIdentityNumber}), tel {PhoneNumber}, {Address}";

}
 