using CareerHelper.Services;

namespace CareerHelper.Menus
{
    internal class TestMenu
    {
        private readonly TestService _tests;
        public TestMenu(TestService tests) { _tests = tests; }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("=== Test Menu ===");
                Console.WriteLine("1. Take Test");
                Console.WriteLine("2. View Last Recommendation");
                Console.WriteLine("3. Back");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": _tests.TakeTest(); break;
                    case "2": _tests.ViewLastRecommendation(); break;
                    case "3": return;
                    default: Console.WriteLine("Invalid."); break;
                }
                Console.WriteLine();
            }
        }
    }
}