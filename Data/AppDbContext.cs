using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TesteWebOllama.Data.Entities;

namespace TesteWebOllama.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<ConversationSummaryEntity> Summaries { get; set; }
    }
}