using System.Text;

using WestcoastEducation.Services;

using WestcoastEducation.UI;
 
Console.OutputEncoding = Encoding.UTF8; 
 
var registry = new SchoolRegistry();

new ConsoleMenu(registry).Run();
 
 
 
                      
