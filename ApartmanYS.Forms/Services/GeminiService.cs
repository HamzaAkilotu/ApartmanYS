using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ApartmanYS.Forms.Helpers;

namespace ApartmanYS.Forms.Services
{
    /// <summary>
    /// Google Gemini 1.5 Flash API servisi
    /// </summary>
    public class GeminiService : IDisposable
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private bool _disposed = false;
        // Denenecek model adları (sırayla)
        private static readonly string[] ModelNames = {
            "gemini-1.5-flash-002",  // En güncel Flash
            "gemini-1.5-flash",      // Standart Flash
            "gemini-2.0-flash-exp",  // Yeni Flash
            "gemini-2.5-flash",      // En yeni Flash
            "gemini-pro"              // Fallback
        };

        public GeminiService()
        {
            _apiKey = EnvHelper.GetGeminiApiKey();
            _httpClient = new HttpClient();
            
            // Debug: API anahtarının okunup okunmadığını kontrol et
            if (string.IsNullOrEmpty(_apiKey))
            {
                System.Diagnostics.Debug.WriteLine("UYARI: Gemini API anahtarı bulunamadı! .env dosyasında GEMINI_API_KEY değişkenini kontrol edin.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Gemini API anahtarı başarıyla yüklendi (uzunluk: {_apiKey.Length})");
            }
        }

        /// <summary>
        /// Gemini API'ye mesaj gönder ve yanıt al
        /// </summary>
        public async Task<string> SendMessageAsync(string prompt, string systemPrompt = null)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "⚠️ API anahtarı bulunamadı. Lütfen .env dosyasına GEMINI_API_KEY değerini ekleyin.\n\n" +
                       "Örnek .env dosyası içeriği:\n" +
                       "GEMINI_API_KEY=AIzaSy...";
            }

            try
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = systemPrompt != null ? $"{systemPrompt}\n\n{prompt}" : prompt }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        topK = 40,
                        topP = 0.95,
                        maxOutputTokens = 2048
                    }
                };

                string json = JsonSerializer.Serialize(requestBody);

                // Modelleri sırayla dene
                string lastError = "";
                
                foreach (string modelName in ModelNames)
                {
                    // Önce v1 API'sini dene
                    string[] apiVersions = { "v1", "v1beta" };
                    
                    foreach (string apiVersion in apiVersions)
                    {
                        // Her denemede yeni StringContent oluştur (dispose hatası önlemek için)
                        using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                        {
                            string url = $"https://generativelanguage.googleapis.com/{apiVersion}/models/{modelName}:generateContent?key={_apiKey}";
                            
                            System.Diagnostics.Debug.WriteLine($"Deneniyor: {apiVersion}/{modelName}");
                            
                            try
                            {
                                using (var response = await _httpClient.PostAsync(url, content))
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"✅ Başarılı: {apiVersion}/{modelName}");
                                        
                                        // Başarılı response'u oku ve döndür
                                        string responseContent = await response.Content.ReadAsStringAsync();
                                        var jsonDoc = JsonDocument.Parse(responseContent);

                                        if (jsonDoc.RootElement.TryGetProperty("candidates", out var candidates) &&
                                            candidates.GetArrayLength() > 0)
                                        {
                                            var firstCandidate = candidates[0];
                                            if (firstCandidate.TryGetProperty("content", out var contentObj) &&
                                                contentObj.TryGetProperty("parts", out var parts) &&
                                                parts.GetArrayLength() > 0)
                                            {
                                                var firstPart = parts[0];
                                                if (firstPart.TryGetProperty("text", out var text))
                                                {
                                                    return text.GetString();
                                                }
                                            }
                                        }
                                        
                                        return "Yanıt alınamadı.";
                                    }
                                    else
                                    {
                                        string errorContent = await response.Content.ReadAsStringAsync();
                                        lastError = errorContent;
                                        System.Diagnostics.Debug.WriteLine($"❌ Başarısız: {apiVersion}/{modelName} - {response.StatusCode}");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lastError = ex.Message;
                                System.Diagnostics.Debug.WriteLine($"❌ Exception: {apiVersion}/{modelName} - {ex.Message}");
                            }
                        }
                    }
                }
                
                // Hiçbir model çalışmadı
                return $"⚠️ Hiçbir model çalışmadı. Son hata: {lastError}\n\n" +
                       $"Lütfen API anahtarınızın geçerli olduğundan ve gerekli izinlere sahip olduğundan emin olun.";
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }

        /// <summary>
        /// Daire analizi için özel prompt
        /// </summary>
        public async Task<string> AnalyzeDebtPredictionAsync(string daireBilgisi, string odemeGecmisi, string mevcutBorclar)
        {
            string systemPrompt = @"Sen bir apartman yönetim sistemi için daire analiz uzmanısın. 
Kullanıcıya Türkçe, net ve anlaşılır analizler ve öneriler sun. 
Analiz sonuçlarını şu formatta sun:
1. Genel Durum Değerlendirmesi
2. Ödeme Geçmişi Analizi
3. Risk Değerlendirmesi (Düşük/Orta/Yüksek)
4. Öneriler";

            string prompt = $@"Aşağıdaki daire bilgilerine göre kapsamlı daire analizi yap:

Daire Bilgisi: {daireBilgisi}
Ödeme Geçmişi: {odemeGecmisi}
Mevcut Borçlar: {mevcutBorclar}

Bu daire için detaylı analiz yap, ödeme alışkanlıklarını değerlendir ve yönetim önerileri sun.";

            return await SendMessageAsync(prompt, systemPrompt);
        }

        /// <summary>
        /// Genel sohbet için mesaj gönder
        /// </summary>
        public async Task<string> ChatAsync(string userMessage, string conversationHistory = null)
        {
            string systemPrompt = @"Sen bir apartman yönetim sistemi asistanısın. 
Kullanıcılara apartman yönetimi, aidat ödemeleri, duyurular, şikayetler gibi konularda yardımcı ol. 
Türkçe, samimi ve yardımsever bir dil kullan. 
Eğer bir soruyu cevaplayamazsan, kullanıcıyı yöneticiye yönlendir.";

            string prompt = conversationHistory != null 
                ? $"{conversationHistory}\n\nKullanıcı: {userMessage}\n\nAsistan:"
                : $"Kullanıcı: {userMessage}\n\nAsistan:";

            return await SendMessageAsync(prompt, systemPrompt);
        }

        /// <summary>
        /// Kaynakları temizle
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
