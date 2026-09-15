using System.Text;
using TesteWebOllama.Models;

namespace TesteWebOllama.Services;

public class PromptBuilder
{
    private const string SystemPrompt =
        """
        Você é um assistente útil, objetivo e educado.
        Responda sempre em português do Brasil.
        Seja claro e direto nas respostas.
        """;

    public string BuildPrompt(List<Message> messages, ConversationSummary? summary)

    {
        var prompt = new StringBuilder();

        prompt.AppendLine(SystemPrompt);
        prompt.AppendLine();

        if (summary is not null)
        {
            prompt.AppendLine("Resumo da conversa:");
            prompt.AppendLine(summary.Summary);
            prompt.AppendLine();
        }

        foreach (var message in messages)
        {
            prompt.AppendLine(
                $"{message.Role}: {message.Content}");
        }

        prompt.AppendLine();
        prompt.Append("assistant:");

        return prompt.ToString();
    }
}