namespace TesteWebOllama.Data.Entities
{
    public class ConversationSummaryEntity
    {
        public int Id { get; set; }

        public string SessionId { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateTime UpdatedAt { get; set; }
    }
}
