using ChatbotAI._Convergence.Common;
using ChatbotAI.Models;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

namespace ChatbotAI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        [HttpPost("GetResponse")]
        public IActionResult GetResponse([FromBody] ChatModel model)
        {
            //https://www.nuget.org/packages/OpenAI/
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            ChatClient client = new(model: "gpt-4.1", apiKey: Constants.ChatGPTKey);
            ChatCompletion completion = client.CompleteChat(model.Request);
            model.Response = completion.Content[0].Text;
            return Ok(model);
        }
    }
}
