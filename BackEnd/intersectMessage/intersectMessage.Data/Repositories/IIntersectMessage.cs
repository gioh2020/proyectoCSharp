using intersectMessage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace intersectMessage.Data.Repositories
{
    public interface IIntersectMessage
    {
        Task<IEnumerable<MessageIntersect>> GetAllMessages();
        Task<MessageIntersect> GetDetails(int id);
        Task<bool> InsertMessage(MessageIntersect messageIntersect);
        Task<bool> DeleteMessage(int id);

    }
}
