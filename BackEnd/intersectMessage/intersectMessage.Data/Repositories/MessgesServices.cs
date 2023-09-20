using Dapper;
using intersectMessage.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<MessageIntersect>> GetAllMessages()
        {
            var db = dbConnetion();
            var sql = @"SELECT messageId, messageNum, satelite
                        FROM messagesIntersect";
            return db.QueryAsync<MessageIntersect>(sql, new { });
        }

        public Task<MessageIntersect> GetDetails(int id)
        { 
            var db = dbConnetion();
            var sql = @"SELECT messageId, messageNum, satelite, AuditDate
                        FROM intersectmessages
                        WHERE id = @Id";
     ;
            return dbConnetion().QueryFirstOrDefaultAsync<MessageIntersect>(sql, new { MessageId = id });
        }

        public Task<object?> GetDitails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertMessage(MessageIntersect messageIntersect)
        {
            throw new NotImplementedException();
        }
    }
}
