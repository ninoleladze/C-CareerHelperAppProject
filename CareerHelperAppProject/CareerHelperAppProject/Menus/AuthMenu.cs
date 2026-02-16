using CareerHelper.Services;

namespace CareerHelper.Menus
{
    internal class AuthMenu
    {
        private readonly UserService _users;
        public AuthMenu(UserService users) { _users = users; }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("=== Auth Menu ===");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Back");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": _users.Register(); break;
                    case "2": _users.Login(); break;
                    case "3": _users.Logout(); break;
                    case "4": return;
                    default: Console.WriteLine("Invalid."); break;
                }
                Console.WriteLine();
            }
        }
    }
}