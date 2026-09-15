using TesteWebOllama.Models;

namespace TesteWebOllama.Service;

public class ConversationService
{
    private readonly Dictionary<string, List<Message>>
        _conversations = new();

    private const int MaxMessages = 20;

    public void AddMessage(
        string sessionId,
        Message message)
    {
        if (!_conversations.ContainsKey(sessionId))
        {
            _conversations[sessionId] =
                new List<Message>();
        }

        _conversations[sessionId]
            .Add(message);

        /*if (_conversations[sessionId].Count > MaxMessages)
        {
            _conversations[sessionId] =
                _conversations[sessionId]
                //.TakeLast(MaxMessages)
                .ToList();
        }*/
    }

    public List<Message> GetMessages(
        string sessionId)
    {
        if (!_conversations.ContainsKey(sessionId))
        {
            return new List<Message>();
        }

        return _conversations[sessionId];
    }
    public bool NeedsSummary(
    string sessionId)
    {
        if (!_conversations.ContainsKey(sessionId))
        {
            return false;
        }

        return _conversations[sessionId].Count >= MaxMessages;
    }

    public void ClearConversation(
        string sessionId)
    {
        if (_conversations.ContainsKey(sessionId))
        {
            _conversations.Remove(sessionId);
        }
    }

    public void TrimHistory(
    string sessionId,
    int keepLastMessages = 5)
    {
        if (!_conversations.ContainsKey(sessionId))
        {
            return;
        }

        _conversations[sessionId] =
            _conversations[sessionId]
                .TakeLast(keepLastMessages)
                .ToList();
    }
}