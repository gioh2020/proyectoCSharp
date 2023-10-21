using intersectMessage.Data.Interfaces;
using intersectMessage.Data.Models;
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
        public async Task<IActionResult> GetMessageIntersectDeatails(int id, int messageid)
        {
            return Ok(await _messageIntersect.GetDetails(id, messageid));
        }
        [HttpPost()]
        public async Task<IActionResult> createMessageInterset([FromBody] MessageIntersectAndSatelites messageIntersectAndSatelites)
        {
            if (messageIntersectAndSatelites == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _messageIntersect.InsertMessage(messageIntersectAndSatelites);
            return Created("created", created);
        
        }

        [HttpPost("createSatelite")]
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
