using CareerHelper.Services;

namespace CareerHelper.Menus
{
    internal class UserMenu
    {
        private readonly UserService _users;
        public UserMenu(UserService users) { _users = users; }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("=== User Menu ===");
                Console.WriteLine("1. View All Users");
                Console.WriteLine("2. Edit Profile");
                Console.WriteLine("3. Change Password");
                Console.WriteLine("4. Delete Account");
                Console.WriteLine("5. View My Results");
                Console.WriteLine("6. View My Profile");   
                Console.WriteLine("7. Back");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": _users.ViewAllUsers(); break;
                    case "2": _users.EditProfile(); break;
                    case "3": _users.ChangePassword(); break;
                    case "4": _users.DeleteAccount(); break;
                    case "5": _users.ViewMyResults(); break;
                    case "6": _users.ViewProfile(); break;
                    case "7": return;
                    default: Console.WriteLine("Invalid."); break;
                }
                Console.WriteLine();
            }
        }
    }
}