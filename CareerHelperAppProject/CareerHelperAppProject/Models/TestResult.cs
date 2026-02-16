namespace CareerHelper.Models
{
    public class TestResult
    {
        public int UserId { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.Now;
        public string TopCategory { get; set; } = "";
        public Dictionary<string, int> Scores { get; set; } = new();
        public List<Profession> Recommended { get; set; } = new();
        public string Strengths { get; set; } = "";
        public string Weaknesses { get; set; } = "";

        public override string ToString()
        {
            string scores = string.Join(", ", Scores.Select(kv => $"{kv.Key}={kv.Value}"));
            return $"UserId={UserId}, TakenAt={TakenAt:g}, TopCategory={TopCategory}, Scores: {scores}";
        }
    }
}