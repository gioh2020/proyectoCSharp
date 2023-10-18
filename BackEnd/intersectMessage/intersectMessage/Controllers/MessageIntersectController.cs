using intersectMessage.Data.Interfaces;
using intersectMessage.Model;
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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _messageIntersect.InsertMessage(messageIntersect);
            return Created("created", created);
        
        }

        [HttpPost]
        public async Task<IActionResult> createSatelite([FromBody] Satelite satelite)
        {
            if (satelite == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _messageIntersect.createSatelite(satelite);
            return Created("created", created);

        }

    }
}
