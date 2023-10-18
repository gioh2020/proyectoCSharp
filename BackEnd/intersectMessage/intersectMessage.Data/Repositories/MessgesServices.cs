using Dapper;
using intersectMessage.Model;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data.Repositories
{
    public class MessagesIntersetServices : IIntersectMessage
    {
        private readonly MySqlConfig _connectionString;
        public MessagesIntersetServices(MySqlConfig connectionString) 
        {
            _connectionString = connectionString;
        }
        protected  MySqlConnection dbConnetion()
        {
            return new MySqlConnection(_connectionString.ConnetionString);
        }
        public Task<bool> DeleteMessage(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MessageIntersect>> GetAllMessages()
        {
            var db = dbConnetion();
            var sql = @"SELECT messageId, messageNum, satelite, message
                        FROM messagesIntersect";
            return await  db.QueryAsync<MessageIntersect>(sql, new { });
        }

        public async Task<MessageIntersect> GetDetails(int id)
        { 
            var db = dbConnetion();
            var sql = @"SELECT messageId, satelite, Message, AuditDate
                        FROM intersectmessages
                        WHERE id = @Id";
     ;
            return await db.QueryFirstOrDefaultAsync<MessageIntersect>(sql, new { MessageId = id });
        }

        public Task<object?> GetDitails(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertMessage(MessageIntersect messageIntersect)

        {
            var db = dbConnetion();
            var sql = @"INSERT INTO messagesIntersect(messageNum, satelite, message) 
                        VALUES(@MessageNum, @Satelite, @Message)";
           var result = await db.ExecuteAsync(sql, new { messageIntersect.MessageNum, messageIntersect.Satelite, messageIntersect.Message });
            return result > 0;
        }
    }
}
