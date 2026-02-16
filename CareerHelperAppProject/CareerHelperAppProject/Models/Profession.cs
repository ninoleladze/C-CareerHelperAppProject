namespace CareerHelper.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> RequiredSkills { get; set; } = new();
        public decimal AverageSalary { get; set; }
        public string LearningPath { get; set; } = "";

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Category}) - Salary: {AverageSalary}";
        }
    }
}