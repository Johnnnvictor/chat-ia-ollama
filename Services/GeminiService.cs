using System.Text;
using System.Text.Json;
using TesteWebOllama.Interfaces;

namespace TesteWebOllama.Service
{
    public class GeminiService : IChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(10);

            _apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException("Chave da API Gemini não configurada.");
        }

        public async Task<string> GerarRespostaAsync(string pergunta)
        {
            var requestBody = new
            {
                systemInstruction = new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = "Você é um assistente especializado em programação C# e .NET. Responda em português, de forma objetiva e didática."
                        }
                    }
                },
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new
                            {
                                text = pergunta
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"Erro ao chamar Gemini: {response.StatusCode}\n{responseText}";
            }

            using var doc = JsonDocument.Parse(responseText);

            var resposta = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return resposta ?? "";
        }
    }
}