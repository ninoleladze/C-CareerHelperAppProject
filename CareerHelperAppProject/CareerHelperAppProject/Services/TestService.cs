using CareerHelper.Models;

namespace CareerHelper.Services
{
    internal class TestService
    {
        private readonly ProfessionService _profs = new();
        private readonly UserService _userService;

        public TestService(UserService userService)
        {
            _userService = userService;
        }

        public void TakeTest()
        {
            if (UserService.CurrentUser == null)
            {
                Console.WriteLine("Login first.");
                return;
            }

            _profs.LoadOrSeed();

            Console.WriteLine("=== Career Test (15 Questions) ===");
            Console.WriteLine("Answer each question with A, B, C, or D.");
            Console.WriteLine();

            int it = 0, med = 0, edu = 0, other = 0;

            Ask("1. Do you enjoy working with computers or technology?",
                "A) Yes, I like coding or building apps",
                "B) Only for medical equipment or research",
                "C) Sometimes, for teaching or research",
                "D) Not really, I prefer creative or practical work",
                ref it, ref med, ref edu, ref other);

            Ask("2. Do you like helping people when they face problems?",
                "A) I prefer solving technical problems",
                "B) Yes, caring for people makes me happy",
                "C) Yes, I enjoy advising or guiding",
                "D) Yes, through creativity, sports, or teamwork",
                ref it, ref med, ref edu, ref other);

            Ask("3. Do you enjoy explaining things to classmates or colleagues?",
                "A) Only technical topics",
                "B) Not much, I prefer caring",
                "C) Yes, I enjoy teaching or advising",
                "D) I prefer showing through action or art",
                ref it, ref med, ref edu, ref other);

            Ask("4. Do you like building or designing things?",
                "A) Programs, robots, or buildings",
                "B) Treatments or safety plans",
                "C) Lessons or legal arguments",
                "D) Artwork, performances, or products",
                ref it, ref med, ref edu, ref other);

            Ask("5. Which place feels most comfortable for you?",
                "A) At a desk with a computer or tools",
                "B) In a hospital, clinic, or lab",
                "C) In a classroom, university, or courtroom",
                "D) In a studio, stadium, or outdoors",
                ref it, ref med, ref edu, ref other);

            Ask("6. Do you enjoy sharing ideas and working together with classmates?",
                "A) Yes, especially when solving technical problems",
                "B) Yes, when helping or supporting people",
                "C) Yes, when teaching, advising, or keeping things fair",
                "D) Yes, in creative, sports, or business activities",
                ref it, ref med, ref edu, ref other);

            Ask("7. Do you like working with numbers and data?",
                "A) Yes, I enjoy analysis and coding",
                "B) Only for medical results or lab work",
                "C) Sometimes, for research or law cases",
                "D) Not much, I prefer creative or practical work",
                ref it, ref med, ref edu, ref other);

            Ask("8. Do you enjoy teaching skills to friends or teammates?",
                "A) Technical or coding skills",
                "B) Health or safety skills",
                "C) Yes, teaching is my strength",
                "D) Creative or sports skills",
                ref it, ref med, ref edu, ref other);

            Ask("9. Do you prefer working alone or in a team?",
                "A) Alone on technical tasks",
                "B) With people, caring or protecting",
                "C) With groups, teaching or advising",
                "D) In teams, sports or creative work",
                ref it, ref med, ref edu, ref other);

            Ask("10. Do you enjoy researching and learning new facts?",
                "A) In technology or engineering",
                "B) In medicine or biology",
                "C) In education or law",
                "D) In arts, sports, or agriculture",
                ref it, ref med, ref edu, ref other);

            Ask("11. Do you like practical, hands-on work?",
                "A) With machines or code",
                "B) With patients or communities",
                "C) With lessons or cases",
                "D) With tools, art, or sports",
                ref it, ref med, ref edu, ref other);

            Ask("12. Do you enjoy supporting friends when they feel stressed?",
                "A) Not really, I prefer technical work",
                "B) Yes, that’s important to me",
                "C) Yes, through advice or teaching",
                "D) Yes, through art, sports, or teamwork",
                ref it, ref med, ref edu, ref other);

            Ask("13. Do you enjoy creating something new?",
                "A) A program or design",
                "B) A treatment or safety plan",
                "C) A lesson or argument",
                "D) An artwork, performance, or product",
                ref it, ref med, ref edu, ref other);

            Ask("14. Do you feel proud when others succeed because of your help?",
                "A) In technical projects",
                "B) In health or safety",
                "C) In learning or justice",
                "D) In art, sports, or business",
                ref it, ref med, ref edu, ref other);

            Ask("15. Which success feels best to you?",
                "A) A system works perfectly",
                "B) A patient recovers or a community is safe",
                "C) A student learns or a case is won",
                "D) A performance, victory, or creation is admired",
                ref it, ref med, ref edu, ref other);

            string top = DecideTop(it, med, edu, other);

            var scores = new Dictionary<string, int>
            {
                ["IT/Engineering"] = it,
                ["Medicine/Public Service"] = med,
                ["Education/Law"] = edu,
                ["Creative/Other"] = other
            };

            var recs = _profs.GetByCategory(top);

            var result = new TestResult
            {
                UserId = UserService.CurrentUser.Id,
                TopCategory = top,
                Scores = scores,
                Recommended = recs,
                Strengths = BuildStrengths(scores),
                Weaknesses = BuildWeaknesses(scores)
            };

            _userService.AttachResult(result);

            Console.WriteLine();
            Console.WriteLine("=== Recommendation ===");
            Console.WriteLine($"Top Category: {top}");
            foreach (var p in recs) Console.WriteLine($" - {p}");
            Console.WriteLine($"Strengths: {result.Strengths}");
            Console.WriteLine($"Weaknesses: {result.Weaknesses}");
        }

        private void Ask(string question, string A, string B, string C, string D,
                         ref int it, ref int med, ref int edu, ref int other)
        {
            Console.WriteLine(question);
            Console.WriteLine(A);
            Console.WriteLine(B);
            Console.WriteLine(C);
            Console.WriteLine(D);
            Console.Write("Your answer (A/B/C/D): ");
            string ans = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (ans == "A") it++;
            else if (ans == "B") med++;
            else if (ans == "C") edu++;
            else if (ans == "D") other++;
            else Console.WriteLine("Invalid input. No points recorded.");

            Console.WriteLine();
        }

        private string DecideTop(int it, int med, int edu, int other)
        {
            int max = new[] { it, med, edu, other }.Max();
            if (max == it) return "IT";
            if (max == med) return "Medicine";
            if (max == edu) return "Education";
            return "Creative";
        }

        public void ViewLastRecommendation()
        {
            if (UserService.CurrentUser == null)
            {
                Console.WriteLine("Login first.");
                return;
            }

            var results = UserService.CurrentUser.TestResults;
            if (!results.Any())
            {
                Console.WriteLine("No test results yet.");
                return;
            }

            var last = results.Last();
            Console.WriteLine("=== Last Recommendation ===");
            Console.WriteLine($"Top Category: {last.TopCategory}");
            foreach (var p in last.Recommended)
                Console.WriteLine($" - {p}");
            Console.WriteLine($"Strengths: {last.Strengths}");
            Console.WriteLine($"Weaknesses: {last.Weaknesses}");
        }

        private string BuildStrengths(Dictionary<string, int> scores)
        {
            int max = scores.Values.Max();
            var strong = scores.Where(kv => kv.Value == max).Select(kv => kv.Key);
            return "Strong interest in: " + string.Join(", ", strong);
        }

        private string BuildWeaknesses(Dictionary<string, int> scores)
        {
            int min = scores.Values.Min();
            var weak = scores.Where(kv => kv.Value == min).Select(kv => kv.Key);
            return "Lower interest in: " + string.Join(", ", weak);
        }
    }
}