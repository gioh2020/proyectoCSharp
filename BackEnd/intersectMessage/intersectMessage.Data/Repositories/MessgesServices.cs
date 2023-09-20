using intersectMessage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data.Repositories
{
    internal class MessagesIntersetServices : IIntersectMessage
    {
        public Task<bool> DeleteMessage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageIntersect>> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public Task<MessageIntersect> GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertMessage(MessageIntersect messageIntersect)
        {
            throw new NotImplementedException();
        }
    }
}
