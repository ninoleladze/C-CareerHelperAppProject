using CareerHelper.Menus;
using CareerHelper.Services;

namespace CareerHelper
{
    internal class Program
    {
        static void Main()
        {
            var userService = new UserService();
            userService.Load();  
            Console.Title = "CareerHelper";
            new MainMenu().Start();
        }
    }
}