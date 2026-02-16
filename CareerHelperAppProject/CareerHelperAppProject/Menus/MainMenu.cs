using CareerHelper.Services;

namespace CareerHelper.Menus
{
    internal class MainMenu
    {
        private readonly UserService _users = new();
        private readonly ProfessionService _profs = new();
        private readonly TestService _tests ;

        public MainMenu()
        {
            _tests = new TestService(_users);
        }


        public void Start()
        {
            _users.Load();
            _profs.LoadOrSeed();

            var auth = new AuthMenu(_users);
            var userMenu = new UserMenu(_users);
            var profMenu = new ProfessionMenu(_profs);
            var testMenu = new TestMenu(_tests);

            while (true)
            {
                Console.WriteLine("=== CareerHelper Main Menu ===");
                Console.WriteLine(_users.GetLoginStatus());
                Console.WriteLine("1. Auth");
                Console.WriteLine("2. User");
                Console.WriteLine("3. Professions");
                Console.WriteLine("4. Test");
                Console.WriteLine("5. About");
                Console.WriteLine("6. Exit");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": auth.Start(); break;
                    case "2": userMenu.Start(); break;
                    case "3": profMenu.Start(); break;
                    case "4": testMenu.Start(); break;
                    case "5":
                        Console.WriteLine("CareerHelper — a simple, robust console app to guide career choices with tests and recommendations.");
                        break;
                    case "6": return;
                    default: Console.WriteLine("Invalid."); break;
                }
                Console.WriteLine();
            }
        }
    }
}