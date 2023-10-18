using intersectMessage.Model;



namespace intersectMessage.Data.Interfaces
{
    public interface IIntersectMessage
    {
        Task<IEnumerable<MessageIntersect>> GetAllMessages();
        Task<MessageIntersect> GetDetails(int id);
        Task<bool> InsertMessage(MessageIntersect messageIntersect);
        Task<bool> DeleteMessage(int id);
        Task<object?> GetDitails(int id);
        Task<bool> createSatelite(Satelite satelite);

    }
}
