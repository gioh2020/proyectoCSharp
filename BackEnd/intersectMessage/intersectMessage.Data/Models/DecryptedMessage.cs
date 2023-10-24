using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data.Models
{
    public class DecryptedMessage
    {
        public int DecryptedMessageId { get; set; }
        public string Message { get; set; }
        public int CoordenadaX{ get; set; }
        public int CoordenadaY { get; set; }
        public int? consecutive { get; set; }

    }
}
