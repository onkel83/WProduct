#pragma warning disable CS8604
using WPAZV.Controller;
using WPBasic;

var controller = new WorktimeController(Settings.GetSetting("WTC") + ".xml");
var _econtroller = new EmployeeController(Settings.GetSetting("EC") + ".xml");

Console.WriteLine("Welcome to the worktime application.");

while (true)
{
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Add worktime entry");
    Console.WriteLine("2. Edit worktime entry");
    Console.WriteLine("3. Delete worktime entry");
    Console.WriteLine("4. View worktime entries");
    Console.WriteLine("5. Add Employer");
    Console.WriteLine("6. Edit Employer");
    Console.WriteLine("7. Delete Employer");
    Console.WriteLine("8. View Employer");
    Console.WriteLine("q. Quit");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            controller.Add();
            break;
        case "2":
            Console.WriteLine("Bitte gib die ID der Arbeitszeit ein :");
            var WTCEid = int.Parse(Console.ReadLine());
            controller.Edit(WTCEid);
            break;
        case "3":
            Console.WriteLine("Bitte gib die ID der Arbeitzeit ein :");
            var WTCDid = int.Parse(Console.ReadLine());
            controller.Delete(WTCDid);
            break;
        case "4":
            controller.ViewWorktime();
            break;
        case "5" :
            break;
        case "6" :
            break;
        case "7" :
            break;
        case "8" :
            break;
        case "q":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}
#pragma warning restore CS8604