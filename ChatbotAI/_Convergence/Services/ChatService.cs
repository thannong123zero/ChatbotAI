using ChatbotAI._Convergence.Hubs;
using ChatbotAI.Models;
using Microsoft.AspNetCore.SignalR;
using Ollama;

namespace ChatbotAI._Convergence.Services
{
    public class ChatService
    {
        private readonly IHubContext<ChatHub> _chatHub;

        public ChatService(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }
        public async Task ChatOllama(ChatModel model)
        {
            using var ollama = new OllamaApiClient();
            if (model.Context == null || model.Context.Count == 0)
            {
                string prompt = "Your name is Lusi, you come from the Kingdom of Lulusia, and your occupation is an English teacher. When you receive a message, you will check its grammar. If the grammar is correct, you will express excitement and encouragement, for example: 'That's right, keep up the good work!' If the grammar is incorrect, you will correct it and point out the grammatical errors in the sentence.\r\nExample:\r\nUser: She is beauti girl!\r\nLusi: Oh dear! Almost perfect, but there's a small mistake. Let's fix it:\r\nIncorrect sentence: She is beauti girl!\r\nCorrected sentence: She is a beautiful girl!\r\nExplanation:\r\n•\t\"Beauti\" is a misspelling. The correct adjective form is \"beautiful\".\r\n•\tAlso, you need the article \"a\" before \"beautiful girl\".\r\nYou're doing great—keep practicing! 🌼 Would you like to try another one?\r\n";
                var initial = await ollama.Completions.GenerateCompletionAsync(model.Model, prompt, stream: false).WaitAsync();
                model.Context = initial.Context;
            }

            var enumerable = ollama.Completions.GenerateCompletionAsync(model.Model, model.Request, context: model.Context);

            await foreach (var response in enumerable)
            {
                _chatHub.Clients.All.SendAsync("ReceiveMessage", response.Response, response.Context);
            }
        }
    }
}
