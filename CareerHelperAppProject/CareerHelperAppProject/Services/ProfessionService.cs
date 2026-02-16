using CareerHelper.Models;

namespace CareerHelper.Services
{
    internal class ProfessionService
    {
        private List<Profession> _professions = new();
        private readonly string _path = "Professions.json";

        public void LoadOrSeed()
        {
            _professions = StorageService.Load<Profession>(_path);
            if (_professions.Any()) return;

            int id = 1;
            void Add(string name, string cat, string desc, decimal salary, string learning, params string[] skills)
            {
                _professions.Add(new Profession
                {
                    Id = id++,
                    Name = name,
                    Category = cat,
                    Description = desc,
                    AverageSalary = salary,
                    LearningPath = learning,
                    RequiredSkills = skills.ToList()
                });
            }

            Add("Software Developer", "IT", "Builds software applications.", 60000, "CS degree or bootcamp; build projects", "Programming", "Algorithms", "Problem-solving");
            Add("Web Developer", "IT", "Creates websites and web apps.", 55000, "HTML/CSS/JS; a modern framework", "HTML", "CSS", "JavaScript");
            Add("Data Analyst", "IT", "Analyzes data to find insights.", 58000, "Statistics; SQL; dashboards", "SQL", "Excel", "Visualization");
            Add("Mobile Developer", "IT", "Builds Android/iOS apps.", 59000, "Kotlin/Swift; UI; app stores", "Kotlin/Swift", "UI", "APIs");
            Add("DevOps Engineer", "IT", "Automates deployment and CI/CD.", 65000, "Cloud; pipelines; scripting", "CI/CD", "Cloud", "Scripting");
            Add("AI Engineer", "IT", "Builds AI models and pipelines.", 70000, "ML; DL; Python", "Python", "ML", "Deep Learning");

            Add("Doctor", "Medicine", "Diagnoses and treats patients.", 90000, "Medical school; residency", "Clinical", "Communication", "Ethics");
            Add("Nurse", "Medicine", "Provides patient care.", 50000, "Nursing program; practice", "Care", "Medical Knowledge", "Communication");
            Add("Biologist", "Medicine", "Studies living organisms.", 56000, "Biology degree; lab work", "Lab", "Research", "Biology");
            Add("Pharmacist", "Medicine", "Prepares and dispenses medication.", 65000, "Pharmacy degree; licensing", "Pharmacology", "Accuracy", "Counseling");
            Add("Lab Technician", "Medicine", "Performs lab tests.", 48000, "Lab training; safety", "Lab", "Measurement", "Quality");
            Add("Psychologist", "Medicine", "Studies behavior and mind.", 60000, "Psychology degree; practice", "Empathy", "Research", "Communication");

            Add("Teacher", "Education", "Teaches students.", 40000, "Teaching practice; pedagogy", "Communication", "Planning", "Mentoring");
            Add("University Lecturer", "Education", "Teaches at university.", 52000, "Advanced degree; research", "Subject Expertise", "Presentation", "Research");
            Add("Tutor", "Education", "Guides students individually.", 35000, "Subject practice; coaching", "Patience", "Explaining", "Organization");
            Add("Instructional Designer", "Education", "Designs learning materials.", 50000, "Learning theory; tools", "Design", "Storyboarding", "Assessment");
            Add("Education Coordinator", "Education", "Manages educational programs.", 45000, "Planning; coordination", "Organization", "Leadership", "Documentation");
            Add("School Counselor", "Education", "Supports students' development.", 42000, "Counseling; guidance", "Empathy", "Communication", "Planning");

            Add("Project Manager", "Business", "Leads projects.", 60000, "PM frameworks; certification", "Planning", "Leadership", "Communication");
            Add("Business Analyst", "Business", "Analyzes business processes.", 60000, "Requirements; data; domain", "Analysis", "Documentation", "SQL/Excel");
            Add("Accountant", "Business", "Manages financial records.", 55000, "Accounting degree; software", "Accounting", "Accuracy", "Excel");
            Add("Marketing Specialist", "Business", "Creates marketing strategies.", 52000, "Digital marketing; content", "Creativity", "Analytics", "Communication");
            Add("Sales Manager", "Business", "Leads sales teams.", 58000, "Negotiation; CRM", "Communication", "Negotiation", "Leadership");

            Add("Graphic Designer", "Creative", "Designs visual content.", 45000, "Design principles; portfolio", "Design", "Creativity", "Typography");
            Add("UI/UX Designer", "Creative", "Designs interfaces and experiences.", 55000, "Research; prototyping", "User Research", "Wireframing", "Prototyping");
            Add("Animator", "Creative", "Creates animations.", 50000, "2D/3D tools; storytelling", "Animation", "Storytelling", "Rendering");
            Add("Video Editor", "Creative", "Edits video content.", 48000, "Editing tools; pacing", "Editing", "Color", "Story");
            Add("Photographer", "Creative", "Captures photos.", 35000, "Camera; editing; portfolio", "Composition", "Editing", "Light");

            Add("Mechanical Engineer", "Engineering", "Designs mechanical systems.", 70000, "ME degree; CAD", "Mechanics", "CAD", "Problem-solving");
            Add("Civil Engineer", "Engineering", "Designs infrastructure.", 70000, "CE degree; codes", "Structures", "Planning", "CAD");
            Add("Electrical Engineer", "Engineering", "Designs electrical systems.", 72000, "EE degree; circuits", "Circuits", "EM", "CAD");
            Add("Robotics Engineer", "Engineering", "Builds robotic systems.", 75000, "Control; sensors; programming", "Control", "Sensors", "Programming");
            Add("Architect", "Engineering", "Designs buildings.", 75000, "Architecture degree; portfolio", "Design", "BIM", "Creativity");

            Add("Lawyer", "Law", "Represents clients in legal matters.", 75000,
                "Law degree; bar exam", "Legal Knowledge", "Research", "Advocacy");

            Add("Judge", "Law", "Presides over court cases.", 90000,
                "Law degree; judicial appointment", "Decision-making", "Legal Knowledge", "Integrity");

            Add("Paralegal", "Law", "Supports lawyers with research and documentation.", 45000,
                "Paralegal training", "Research", "Documentation", "Organization");

            Add("Prosecutor", "Law", "Represents the state in criminal cases.", 80000,
                "Law degree; bar exam", "Legal Knowledge", "Public Speaking", "Ethics");

            Add("Defense Attorney", "Law", "Defends individuals or organizations accused of crimes.", 78000,
                "Law degree; bar exam", "Advocacy", "Negotiation", "Legal Knowledge");

            Add("Athlete", "Sports", "Competes in sports professionally.", 50000, "Training; discipline", "Fitness", "Discipline", "Teamwork");
            Add("Coach", "Sports", "Trains and guides athletes.", 48000, "Sports science; experience", "Leadership", "Strategy", "Motivation");
            Add("Sports Analyst", "Sports", "Analyzes sports performance.", 52000, "Statistics; sports knowledge", "Analysis", "Communication", "Observation");

            StorageService.Save(_path, _professions);
        }

        public void ShowAll()
        {
            if (!_professions.Any()) { Console.WriteLine("No professions available."); return; }
            foreach (var p in _professions) Console.WriteLine(p);
        }

        public void SearchByName()
        {
            Console.Write("Search term: "); var term = Console.ReadLine() ?? "";
            var res = _professions.Where(p => p.Name.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!res.Any()) { Console.WriteLine("No matches."); return; }
            foreach (var p in res) Console.WriteLine(p);
        }
        public List<Profession> GetByCategoryTop(string category, int count = 3)
        {
            var res = _professions
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Take(count)
                .ToList();

            if (res.Count < count)
            {
                var extras = _professions
                    .Where(p => !p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .Take(count - res.Count)
                    .ToList();

                res.AddRange(extras);
            }

            return res;
        }
        public void FilterByCategory()
        {
            Console.Write("Category: "); var cat = Console.ReadLine() ?? "";
            var res = _professions.Where(p => p.Category.Equals(cat, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!res.Any())
            {
                var cats = _professions.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
                Console.WriteLine("No matches. Try one of:");
                foreach (var c in cats) Console.WriteLine($" - {c}");
                return;
            }
            foreach (var p in res) Console.WriteLine(p);
        }

        public void ViewDetails()
        {
            Console.Write("Id: "); int.TryParse(Console.ReadLine(), out int id);
            var p = _professions.FirstOrDefault(x => x.Id == id);
            if (p == null) { Console.WriteLine("Not found."); return; }

            Console.WriteLine($"[{p.Id}] {p.Name} ({p.Category})");
            Console.WriteLine($"Description: {p.Description}");
            Console.WriteLine($"Average Salary: {p.AverageSalary}");
            Console.WriteLine($"Required Skills: {string.Join(", ", p.RequiredSkills)}");
            Console.WriteLine($"Learning Path: {p.LearningPath}");
        }

        public List<Profession> GetByCategory(string cat)
        {
            return _professions.Where(p => p.Category.Equals(cat, StringComparison.OrdinalIgnoreCase)).Take(3).ToList();
        }
    }
}