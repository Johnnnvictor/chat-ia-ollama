using TesteWebOllama.Interfaces;
using TesteWebOllama.Service;
using TesteWebOllama.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddSingleton<IChatService, GeminiService>();
//Quando chamado, o serviço GeminiService será injetado em qualquer lugar que dependa de IChatService.

builder.Services.AddSingleton<IChatService, OllamaService>();
//Quando chamado, o serviço OllamaService será injetado em qualquer lugar que dependa de IChatService.
builder.Services.AddSingleton<ConversationService>();
//Recupera o histórico de conversas, garantindo que as mensagens anteriores sejam mantidas durante a interação com o modelo.
builder.Services.AddSingleton<PromptBuilder>();
//responsável por construir o prompt a partir do histórico de mensagens, garantindo que o modelo receba o contexto completo da conversa.
builder.Services.AddSingleton<ConversationSummaryService>();
//resume o histórico de msgs quando atingir o limite



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
