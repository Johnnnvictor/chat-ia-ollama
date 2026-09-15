using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;
using TesteWebOllama.Interfaces;
using TesteWebOllama.Models;
using TesteWebOllama.Service;
using TesteWebOllama.Services;

namespace TesteWebOllama.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly ConversationService _conversationService;
    private readonly PromptBuilder _promptBuilder;
    private readonly ConversationSummaryService _conversationSummaryService;

    public ChatController(IChatService chatService, ConversationService conversationService, 
        PromptBuilder promptBuilder, ConversationSummaryService conversationSummaryService)
    {
        _chatService = chatService;
        _conversationService = conversationService;
        _promptBuilder = promptBuilder;
        _conversationSummaryService = conversationSummaryService;

    }

    [HttpPost]
    public async Task<ActionResult<ChatResponse>> Chat(ChatRequest request)
    {
        {
            _conversationService.AddMessage(
            request.SessionId,
            new Message
            {
                Role = "user",
                Content = request.Pergunta
            });

            ConversationSummary? summary =
                _conversationSummaryService.GetSummary(
                request.SessionId);

                if (_conversationService.NeedsSummary(request.SessionId))
                     {
                var messages =
                    _conversationService.GetMessages(
                        request.SessionId);

                var generatedSummary =
                    await _conversationSummaryService
                        .GenerateSummary(messages);

                _conversationSummaryService.SaveSummary(
                    request.SessionId,
                    generatedSummary);

                _conversationService.TrimHistory(
                    request.SessionId);

                summary =
                    _conversationSummaryService.GetSummary(
                        request.SessionId);
            }

            var history =
                _conversationService.GetMessages(
                    request.SessionId);

            var prompt =
                _promptBuilder.BuildPrompt(
                    history,
                    summary);

            var resposta =
            await _chatService.GerarRespostaAsync(
            prompt);

            _conversationService.AddMessage(
            request.SessionId,
            new Message
            {
                Role = "assistant",
                Content = resposta
            });

            return Ok(new ChatResponse
            {
                Resposta = resposta
            });
        }
    }



    /*[HttpPost("gemini")]
    public async Task<ActionResult<ChatResponse>> ChatGemini(ChatRequest request)
    {
        var resposta =
            await _geminiService.GerarRespostaAsync(request.Pergunta);

        return Ok(new ChatResponse
        {
            Resposta = resposta
        });
    }*/
}
    