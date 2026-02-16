using CareerHelper.Models;

namespace CareerHelper.Services
{
    internal class UserService
    {
        private List<User> _users = new();
        private readonly string _path = "Users.json";
        public static User? CurrentUser { get; private set; }

        public void Load()
        {
            _users = StorageService.Load<User>(_path);
            int nextId = (_users.Any() ? _users.Max(u => u.Id) : 0) + 1;
            User.ResetCounter(nextId);
        }

        public void Register()
        {
            Console.Write("Name: "); 
            var name = Console.ReadLine() ?? "";
            Console.Write("Age: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? "";
            Console.Write("Password: "); 
            var pwd = Console.ReadLine() ?? "";
            Console.Write("Education Level: ");
            var edu = Console.ReadLine() ?? "";
            Console.Write("Interests: "); 
            var interests = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name) || age <= 0 || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pwd))
            {
                Console.WriteLine("Error: Name, Age, Email, and Password are required. Age must be positive.");
                return;
            }
            if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Error: Email already registered.");
                return;
            }

            var user = new User { Name = name.Trim(), Age = age, Email = email.Trim(), Password = pwd, EducationLevel = edu.Trim(), Interests = interests.Trim() };
            _users.Add(user);
            StorageService.Save(_path, _users);
            Console.WriteLine("User registered successfully.");
        }

        public void Login()
        {
            if (CurrentUser != null) { Console.WriteLine($"Already logged in as {CurrentUser.Name}."); return; }

            Console.Write("Email: ");
            var email = Console.ReadLine() ?? "";
            Console.Write("Password: ");
            var pwd = Console.ReadLine() ?? "";

            var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == pwd);
            if (user == null) { Console.WriteLine("Invalid credentials."); return; }

            CurrentUser = user;
            Console.WriteLine($"Login successful. Welcome, {user.Name}!");
        }

        public void Logout()
        {
            if (CurrentUser == null) { Console.WriteLine("No user logged in."); return; }
            Console.WriteLine($"Goodbye, {CurrentUser.Name}.");
            CurrentUser = null;
        }

        public void ViewAllUsers()
        {
            if (!_users.Any()) { Console.WriteLine("No users found."); return; }
            Console.WriteLine("=== All Users ===");
            foreach (var u in _users) Console.WriteLine(u);
        }

        public void EditProfile()
        {
            if (CurrentUser == null) { Console.WriteLine("Login first."); return; }

            Console.Write("New name (empty=keep): "); 
            var n = Console.ReadLine();
            Console.Write("New age (empty=keep): "); 
            var a = Console.ReadLine();
            Console.Write("New email (empty=keep): ");
            var e = Console.ReadLine();
            Console.Write("New education (empty=keep): ");
            var ed = Console.ReadLine();
            Console.Write("New interests (empty=keep): ");
            var i = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(n)) CurrentUser.Name = n.Trim();
            if (int.TryParse(a, out int age) && age > 0) CurrentUser.Age = age;
            if (!string.IsNullOrWhiteSpace(e))
            {
                var email = e.Trim();
                if (_users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Id != CurrentUser.Id))
                {
                    Console.WriteLine("Error: Email already used by another account.");
                }
                else
                {
                    CurrentUser.Email = email;
                }
            }
            if (!string.IsNullOrWhiteSpace(ed)) CurrentUser.EducationLevel = ed.Trim();
            if (!string.IsNullOrWhiteSpace(i)) CurrentUser.Interests = i.Trim();

            StorageService.Save(_path, _users);
            Console.WriteLine("Profile updated.");
        }

        public void ChangePassword()
        {
            if (CurrentUser == null) { Console.WriteLine("Login first."); return; }

            Console.Write("Current password: "); var cp = Console.ReadLine() ?? "";
            if (cp != CurrentUser.Password) { Console.WriteLine("Error: Incorrect current password."); return; }

            Console.Write("New password: "); var np = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(np)) { Console.WriteLine("Error: New password cannot be empty."); return; }

            CurrentUser.Password = np;
            StorageService.Save(_path, _users);
            Console.WriteLine("Password changed.");
        }

        public void DeleteAccount()
        {
            if (CurrentUser == null) { Console.WriteLine("Login first."); return; }

            Console.Write("Confirm delete (Y/N): "); var c = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
            if (c != "Y") { Console.WriteLine("Deletion cancelled."); return; }

            _users.RemoveAll(u => u.Id == CurrentUser.Id);
            StorageService.Save(_path, _users);
            Console.WriteLine("Account deleted.");
            CurrentUser = null;
        }

        public void ViewMyResults()
        {
            if (CurrentUser == null) { Console.WriteLine("Login first."); return; }
            if (!CurrentUser.TestResults.Any()) { Console.WriteLine("No results found."); return; }

            Console.WriteLine("=== My Results ===");
            foreach (var r in CurrentUser.TestResults)
            {
                Console.WriteLine(r);
                foreach (var p in r.Recommended) Console.WriteLine($" - {p}");
            }
        }
        public void ViewProfile()
        {
            if (CurrentUser == null)
            {
                Console.WriteLine("Login first.");
                return;
            }

            Console.WriteLine("=== My Profile ===");
            Console.WriteLine($"ID: {CurrentUser.Id}");
            Console.WriteLine($"Name: {CurrentUser.Name}");
            Console.WriteLine($"Age: {CurrentUser.Age}");
            Console.WriteLine($"Email: {CurrentUser.Email}");
            Console.WriteLine($"Education Level: {CurrentUser.EducationLevel}");
            Console.WriteLine($"Interests: {CurrentUser.Interests}");

            if (CurrentUser.TestResults.Any())
            {
                Console.WriteLine($"Total Tests Taken: {CurrentUser.TestResults.Count}");
                var last = CurrentUser.TestResults.Last();
                Console.WriteLine($"Last Top Category: {last.TopCategory}");
            }
            else
            {
                Console.WriteLine("No test results yet.");
            }
        }
        public string GetLoginStatus()
        {
            if (CurrentUser == null) return "Not logged in.";
            return $"Logged in as: {CurrentUser.Name} (Email: {CurrentUser.Email})";
        }
        public void AttachResult(TestResult result)
        {
            if (CurrentUser == null) return;
            CurrentUser.TestResults.Add(result);
            StorageService.Save(_path, _users);
        }
    }
}