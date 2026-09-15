namespace TesteWebOllama.Data.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }

        public string SessionId { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
