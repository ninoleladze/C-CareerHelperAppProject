using CareerHelper.Models;

namespace CareerHelper.Services
{
    internal class RecommendationService
    {
        public List<Profession> RecommendCareers(Dictionary<string, int> scores, ProfessionService professionService)
        {
            if (scores == null || !scores.Any()) return new List<Profession>();

            var maxScore = scores.Values.Max();
            var topCategories = scores.Where(kv => kv.Value == maxScore).Select(kv => kv.Key).ToList();
            string primaryCategory = topCategories.OrderBy(c => c).First();

            var primary = professionService.GetByCategoryTop(primaryCategory, 1);
            var alternatives = new List<Profession>();

            var orderedCats = scores
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .Select(kv => kv.Key)
                .ToList();

            foreach (var cat in orderedCats)
            {
                if (cat == primaryCategory) continue;
                var pick = professionService.GetByCategoryTop(cat, 1);
                if (pick.Any()) alternatives.AddRange(pick);
                if (alternatives.Count >= 2) break;
            }

            if (alternatives.Count < 2)
            {
                var sameMore = professionService.GetByCategoryTop(primaryCategory, 3)
                                                .Skip(1).Take(2 - alternatives.Count);
                alternatives.AddRange(sameMore);
            }

            return primary.Concat(alternatives).Take(3).ToList();
        }

        public (string strengths, string weaknesses) AnalyzeStrengthsWeaknesses(Dictionary<string, int> scores)
        {
            if (scores == null || !scores.Any()) return ("", "");

            int max = scores.Values.Max();
            int min = scores.Values.Min();

            var strong = scores.Where(kv => kv.Value == max).Select(kv => kv.Key).ToList();
            var weak = scores.Where(kv => kv.Value == min).Select(kv => kv.Key).ToList();

            string strengths = $"Strong interest in: {string.Join(", ", strong)}.";
            string weaknesses = $"Lower interest in: {string.Join(", ", weak)}.";

            return (strengths, weaknesses);
        }

        public string BuildLearningPathSummary(Profession profession)
        {
            return $"Suggested learning path for {profession.Name}: {profession.LearningPath}\n" +
                   $"Key skills to focus on: {string.Join(", ", profession.RequiredSkills)}";
        }
    }
}