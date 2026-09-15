using System.Text;
using System.Text.Json;
using TesteWebOllama.Interfaces;

namespace TesteWebOllama.Service
{
    public class OllamaService : IChatService
    {
        private readonly HttpClient _httpClient;

        public OllamaService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(2);
        }

        public async Task<string> GerarRespostaAsync(string prompt)
        //esse método envia o prompt para o modelo LLaMA e retorna a resposta gerada
        {
            var requestBody = new
            {
                model = "llama3.2",
                prompt = prompt,
                stream = true
            };

            var json = JsonSerializer.Serialize(requestBody);
            // transforma o input em JSON para enviar para o modelo

            var request = new HttpRequestMessage(
            HttpMethod.Post,
            "http://localhost:11434/api/generate");

            request.Content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead);

            using var stream = await response.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(stream);

            string respostaCompleta = "";


            while (!reader.EndOfStream)
            // lê a resposta do modelo linha por linha
            {
                var linha = await reader.ReadLineAsync();

                //Console.WriteLine($"DEBUG: {linha}");

                if (string.IsNullOrWhiteSpace(linha))
                    continue;

                using var doc = JsonDocument.Parse(linha);

                if (doc.RootElement.TryGetProperty("response", out var token))
                {
                    var texto = token.GetString();

                    respostaCompleta += texto;
                }

                if (doc.RootElement.TryGetProperty("done", out var doneProp) &&
                    doneProp.GetBoolean())
                {
                    break;
                }

            }
            return respostaCompleta;
        }


    }
}
