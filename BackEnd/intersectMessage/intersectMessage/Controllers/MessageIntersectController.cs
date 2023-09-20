using IntersectMessage.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace intersectMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageIntersectController : ControllerBase
    {
        private readonly IIntersectMessage _messageIntersect;

        public MessageIntersectController(IIntersectMessage msgIntersect)
        {
            _messageIntersect = msgIntersect;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            return Ok(await _messageIntersect.GetAllMessages());

        }
        [HttpGet("id")]
        public async Task<IActionResult> GetMessageIntersectDeatails(int id)
        {
            return Ok(await _messageIntersect.GetDitails(id));
        }
        [HttpPost]
        public async Task<IActionResult> createMessageInterset([FromBody] MessageIntersect messageIntersect)
        {
            if (messageIntersect == null)
            {
                return BadRequest();
            }
            if (!MOdelState.IsValid)
            {
                return BadRequest();
            }
        
        }

    }
}
