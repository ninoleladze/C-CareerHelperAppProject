using CareerHelper.Services;

namespace CareerHelper.Menus
{
    internal class ProfessionMenu
    {
        private readonly ProfessionService _profs;
        public ProfessionMenu(ProfessionService profs) { _profs = profs; }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("=== Profession Menu ===");
                Console.WriteLine("1. Show All");
                Console.WriteLine("2. Search by Name");
                Console.WriteLine("3. Filter by Category");
                Console.WriteLine("4. View Details");
                Console.WriteLine("5. Back");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": _profs.ShowAll(); break;
                    case "2": _profs.SearchByName(); break;
                    case "3": _profs.FilterByCategory(); break;
                    case "4": _profs.ViewDetails(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid."); break;
                }
                Console.WriteLine();
            }
        }
    }
}