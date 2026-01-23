using System;
using System.IO;
using System.Windows.Forms;
using ApartmanYS.Forms.Helpers;

namespace ApartmanYS.Forms
{
    /// <summary>
    /// Uygulamanın giriş noktası
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // .env dosyasının yolunu ayarla
            SetupEnvFilePath();
            
            // Login ekranı ile başla
            Application.Run(new Forms.FrmLogin());
        }

        /// <summary>
        /// .env dosyasının yolunu bul ve ayarla
        /// </summary>
        private static void SetupEnvFilePath()
        {
            try
            {
                // Önce çalışma dizininde dene
                string currentDir = Directory.GetCurrentDirectory();
                string envPath = Path.Combine(currentDir, ".env");
                
                if (File.Exists(envPath))
                {
                    EnvHelper.SetEnvFilePath(envPath);
                    return;
                }

                // Solution dizininde dene (executable'dan 4 seviye yukarı)
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string[] possiblePaths = {
                    Path.Combine(baseDir, "..", "..", "..", ".env"),  // bin\Debug\net48\..\..\..\.env
                    Path.Combine(baseDir, "..", "..", ".env"),        // bin\Debug\..\..\.env
                    Path.Combine(baseDir, ".env"),                     // bin\Debug\net48\.env
                    Path.Combine(currentDir, "..", ".env"),           // Çalışma dizininden bir üst
                    Path.Combine(currentDir, "..", "..", ".env")     // Çalışma dizininden 2 üst
                };

                foreach (string path in possiblePaths)
                {
                    try
                    {
                        string fullPath = Path.GetFullPath(path);
                        if (File.Exists(fullPath))
                        {
                            EnvHelper.SetEnvFilePath(fullPath);
                            System.Diagnostics.Debug.WriteLine($"Env dosyası bulundu ve ayarlandı: {fullPath}");
                            return;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                System.Diagnostics.Debug.WriteLine("Uyarı: .env dosyası bulunamadı!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Env dosyası ayarlanırken hata: {ex.Message}");
            }
        }
    }
}