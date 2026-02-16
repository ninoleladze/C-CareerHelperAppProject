namespace CareerHelper.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Category { get; set; } = "";
        public string QuestionText { get; set; } = "";
        public Dictionary<string, int> Options { get; set; } = new();

        public override string ToString()
        {
            return $"Q{Id}: {QuestionText}";
        }
    }
}