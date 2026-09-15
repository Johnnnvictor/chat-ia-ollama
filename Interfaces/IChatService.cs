namespace TesteWebOllama.Interfaces
{
    public interface IChatService
    {
        Task<string> GerarRespostaAsync(string prompt);
    }
}
