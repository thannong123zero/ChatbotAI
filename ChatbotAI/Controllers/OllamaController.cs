using ChatbotAI._Convergence.Services;
using ChatbotAI.Models;
using Microsoft.AspNetCore.Mvc;
using Ollama;

namespace ChatbotAI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OllamaController : ControllerBase
    {
        private readonly ChatService _chatService;
        public OllamaController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("GetResponse")]
        public async Task<IActionResult> GetResponse([FromBody] ChatModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            using var ollama = new OllamaApiClient();
            var result = await ollama.Completions
                .GenerateCompletionAsync(model.Model, model.Request, stream: model.Stream, context: model.Context)
                .WaitAsync();
            model.Response = result.Response;
            model.Context = result.Context;

            return Ok(model);
        }
        [HttpPost("GetStreamingResponse")]
        public IActionResult GetStreamingResponse([FromBody] ChatModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            _chatService.ChatOllama(model);
            return Ok(model);
        }
    }
}
