﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Model
{
    public class MessageIntersect
    {
        public int MessageId { get; set; }
        public int SateliteIdRef { get; set; }
        public decimal Distance { get; set; }
        public int consecutive {  get; set; }   
        public string? Message { get; set; }
    }
}
