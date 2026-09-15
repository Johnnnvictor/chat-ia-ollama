# Chat IA com Ollama

Projeto pessoal desenvolvido para estudar aplicações de Inteligência Artificial com ASP.NET Core, utilizando Ollama como provedor de LLM local e seguindo conceitos de arquitetura utilizados em aplicações modernas baseadas em IA.

## Funcionalidades Implementadas

- Chat conversacional integrado ao Ollama
- Interface web com HTML, CSS e JavaScript
- Web API em ASP.NET Core
- Histórico de conversação por sessão
- Geração automática de SessionId
- Memória curta baseada nas mensagens recentes
- Memória longa através de resumos automáticos
- Prompt Builder para composição dinâmica de contexto
- Injeção de Dependência (Dependency Injection)
- Arquitetura baseada em serviços
- Abstração de provedores através da interface `IChatService`
- Suporte para múltiplos provedores de IA (Ollama e Gemini)

## Arquitetura Atual

Frontend
↓
ChatController
↓
ConversationService
↓
ConversationSummaryService
↓
PromptBuilder
↓
IChatService
↓
OllamaService / GeminiService
↓
LLM

## Tecnologias Utilizadas

- C#
- ASP.NET Core 8
- Ollama
- Llama 3.2
- Gemini API
- HTML
- CSS
- JavaScript
- Entity Framework Core (em implementação)
- SQLite (em implementação)

## Possíveis Melhorias

### Persistência

- Persistência de conversas com SQLite
- Persistência de resumos
- Recuperação de histórico após reinicialização da aplicação

### Inteligência Artificial

- Upload e processamento de PDFs
- Implementação de RAG (Retrieval-Augmented Generation)
- Embeddings vetoriais
- Banco de dados vetorial
- Tool Calling
- Agentes de IA

### Arquitetura

- Repositórios para acesso a dados
- Separação entre Domain, Application e Infrastructure
- Testes unitários
- Logs estruturados
- Configuração por ambiente
- Containerização com Docker

## Objetivo

Evoluir gradualmente de um chat simples para uma aplicação de IA mais próxima de cenários reais de mercado, explorando conceitos como memória conversacional, persistência, RAG, agentes e múltiplos provedores de modelos de linguagem.
