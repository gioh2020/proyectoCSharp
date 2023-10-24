using intersectMessage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace intersectMessage.Data.Models
{
    public class MessageIntersectAndSatelites
    {
 
        public List<Satelite> Satelites { get; set; }
        public List<MessageIntersect> Messages{get; set; }
    }
}
