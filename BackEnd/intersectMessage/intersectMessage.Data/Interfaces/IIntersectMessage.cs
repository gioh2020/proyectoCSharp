using intersectMessage.Data.Models;
using intersectMessage.Model;



namespace intersectMessage.Data.Interfaces
{
    public interface IIntersectMessage
    {
        Task<IEnumerable<MessageIntersect>> GetAllMessages();
        Task<object> GetDetails(int id, int messageId);
        Task<bool> InsertMessage(MessageIntersectAndSatelites messageIntersectAndSatelites);
        Task<bool> DeleteMessage(int id);
        Task<bool> createSatelite(Satelite satelite);

    }
}
