using intersectMessage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data.Models
{
    public class MessageIntersectAndSatelites
    {
        public list<Satelite> Satelites { get; set; }
        public list<MessageIntersect> Messages{get; set; }
    }
}
