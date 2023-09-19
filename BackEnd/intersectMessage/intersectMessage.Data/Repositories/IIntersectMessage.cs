using intersectMessage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace intersectMessage.Data.Repositories
{
    internal interface IIntersectMessage
    {
        Task<IEnumerable<Sato>> GetAllMesage();
    }
}
