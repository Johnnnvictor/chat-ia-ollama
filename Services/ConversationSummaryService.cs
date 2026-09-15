using TesteWebOllama.Interfaces;
using TesteWebOllama.Models;

namespace TesteWebOllama.Services;

public class ConversationSummaryService
{
    private readonly Dictionary<string, ConversationSummary>
        _summaries = new();

    private readonly IChatService _chatService;

    public ConversationSummaryService(
    IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<string> GenerateSummary(
    List<Message> messages)
    {
        var history = string.Join(
            Environment.NewLine,
            messages.Select(message =>
                $"{message.Role}: {message.Content}"));

        var prompt = $"""
        Você é um sistema de sumarização.

        Resuma a conversa abaixo em tópicos curtos.

        Inclua apenas:

        - Informações pessoais importantes
        - Objetivos do usuário
        - Projetos em andamento
        - Tecnologias utilizadas
        - Preferências relevantes

        Ignore cumprimentos, mensagens repetidas e conversas casuais.

        Conversa:

        {history}
        """;

        var summary = await _chatService.GerarRespostaAsync(prompt);

        return summary;
    }

    public void SaveSummary(
        string sessionId,
        string summary)
    {
        _summaries[sessionId] = new ConversationSummary
        {
            SessionId = sessionId,
            Summary = summary
        };
    }

    public ConversationSummary? GetSummary(
        string sessionId)
    {
        if (!_summaries.ContainsKey(sessionId))
        {
            return null;
        }

        return _summaries[sessionId];
    }
}