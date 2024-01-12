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
        public int CoordinateX { get; set; }
        public float CoordinateY { get; set; }
        public float? consecutive { get; set; }
        public DateTime? AuditDate { get; set; }

    }
}
