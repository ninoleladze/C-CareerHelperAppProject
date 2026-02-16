namespace CareerHelper.Models
{
    public class User
    {
        private static int _count = 1;

        public int Id { get; private set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string EducationLevel { get; set; } = "";
        public string Interests { get; set; } = "";
        public List<TestResult> TestResults { get; set; } = new();

        public User() => Id = _count++;

        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Age={Age}, Email={Email}, Education={EducationLevel}, Interests={Interests}";
        }

        public static void ResetCounter(int startFrom) => _count = startFrom;
    }
}