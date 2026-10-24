namespace WestcoastEducation.Models;

public class Address
{
    public string Street { get; set; } = "";
    public string Postalcode { get; set; } = "";
    public string City { get; set; } = "";

    public override string ToString() => $"{Street}, {Postalcode}, {City}";
}
