using System.Text.Json;

namespace CareerHelper.Services
{
    internal static class StorageService
    {
        public static List<T> Load<T>(string path)
        {
            try
            {
                if (!File.Exists(path)) return new List<T>();
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch
            {
                Console.WriteLine($"Warning: Failed to load {path}. Starting empty.");
                return new List<T>();
            }
        }

        public static void Save<T>(string path, List<T> data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch
            {
                Console.WriteLine($"Error: Could not save {path}.");
            }
        }
    }
}