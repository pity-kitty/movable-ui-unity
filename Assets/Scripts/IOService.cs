using System.IO;

namespace EdCon.MiniGameTemplate
{
    public static class IOService
    {
        public static bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        
        public static void SaveData(string filePath, string jsonData)
        {
            File.WriteAllText(filePath, jsonData);
        }

        public static string ReadData(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}