using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data
{
    public class MySqlConfig
    {
        public MySqlConfig()
        {
        }

        public MySqlConfig(string connetionString)
        {
            ConnetionString = connetionString;
        }

        public string ConnetionString { get; set; }
    }
}
