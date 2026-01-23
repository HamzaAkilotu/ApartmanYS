using System;
using System.IO;

namespace ApartmanYS.Forms.Helpers
{
    /// <summary>
    /// .env dosyasından değişkenleri okumak için helper sınıf
    /// </summary>
    public static class EnvHelper
    {
        private static string _envFilePath = null;

        /// <summary>
        /// .env dosyasının yolunu ayarla
        /// </summary>
        public static void SetEnvFilePath(string path)
        {
            _envFilePath = path;
        }

        /// <summary>
        /// .env dosyasından değişken değerini oku
        /// </summary>
        public static string GetEnvVariable(string key, string defaultValue = null)
        {
            try
            {
                // Önce manuel ayarlanmış yolu kontrol et
                if (!string.IsNullOrEmpty(_envFilePath) && File.Exists(_envFilePath))
                {
                    return ReadEnvFile(_envFilePath, key, defaultValue);
                }

                // Farklı olası konumları dene
                string[] possiblePaths = {
                    // Proje kök dizini (bin'den 3 seviye yukarı)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".env"),
                    // Proje kök dizini (bin'den 2 seviye yukarı - Debug klasörü varsa)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".env"),
                    // Çalışma dizini
                    Path.Combine(Directory.GetCurrentDirectory(), ".env"),
                    // Base directory
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env"),
                    // Solution dizini (executable'dan 4 seviye yukarı)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"),
                    // Visual Studio çalışma dizini
                    Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "..", "..", "..", ".env")
                };

                foreach (string envPath in possiblePaths)
                {
                    try
                    {
                        string normalizedPath = Path.GetFullPath(envPath);
                        if (File.Exists(normalizedPath))
                        {
                            System.Diagnostics.Debug.WriteLine($"Env dosyası bulundu: {normalizedPath}");
                            return ReadEnvFile(normalizedPath, key, defaultValue);
                        }
                    }
                    catch
                    {
                        // Bu yolu atla, diğerini dene
                        continue;
                    }
                }

                System.Diagnostics.Debug.WriteLine("Env dosyası hiçbir konumda bulunamadı!");
                return defaultValue;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Env dosyası okuma hatası: {ex.Message}");
                return defaultValue;
            }
        }

        /// <summary>
        /// .env dosyasını oku ve değeri döndür
        /// </summary>
        private static string ReadEnvFile(string envPath, string key, string defaultValue)
        {
            if (!File.Exists(envPath))
            {
                return defaultValue;
            }

            // UTF-8 encoding ile oku
            string[] lines = File.ReadAllLines(envPath, System.Text.Encoding.UTF8);
            
            foreach (string line in lines)
            {
                // Yorum satırlarını ve boş satırları atla
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
                    continue;

                // KEY=VALUE formatını parse et
                int equalsIndex = trimmedLine.IndexOf('=');
                if (equalsIndex > 0)
                {
                    string lineKey = trimmedLine.Substring(0, equalsIndex).Trim();
                    string lineValue = trimmedLine.Substring(equalsIndex + 1).Trim();

                    // Tırnak işaretlerini kaldır
                    if (lineValue.StartsWith("\"") && lineValue.EndsWith("\""))
                    {
                        lineValue = lineValue.Substring(1, lineValue.Length - 2);
                    }
                    else if (lineValue.StartsWith("'") && lineValue.EndsWith("'"))
                    {
                        lineValue = lineValue.Substring(1, lineValue.Length - 2);
                    }

                    if (lineKey.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        System.Diagnostics.Debug.WriteLine($"Env değişkeni bulundu: {key} = {lineValue.Substring(0, Math.Min(10, lineValue.Length))}...");
                        return lineValue;
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Gemini API anahtarını al
        /// </summary>
        public static string GetGeminiApiKey()
        {
            return GetEnvVariable("GEMINI_API_KEY");
        }
    }
}
