using intersectMessage.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace intersectMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageIntersectController : ControllerBase
    {
        private readonly IIntersectMessage _messageIntersect;

        public MessageIntersectController(IIntersectMessage messageIntersect)
        {
            _messageIntersect = messageIntersect;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages() 
        {
            return Ok(await _messageIntersect.GetAllMessages());
            
        }
    }
}
