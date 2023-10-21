using Dapper;
using intersectMessage.Data.Interfaces;
using intersectMessage.Model;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data.Sevices
{
    public class MessagesIntersetServices : IIntersectMessage
    {
        private readonly MySqlConfig _connectionString;
        public MessagesIntersetServices(MySqlConfig connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnetion()
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
            var sql = @"SELECT messageId, messageNum, message
                        FROM messagesIntersect";
            return await db.QueryAsync<MessageIntersect>(sql, new { });
        }

        public async Task<object> GetDetails(int id,int messageid)
        {
            dynamic mesagge = new ExpandoObject();
            var messageDetails = new List<object>();

            var db = dbConnetion();
            string query = "SELECT * FROM messagesIntersect WHERE Consecutive = @Consecutive";

            var results = await db.QueryAsync<MessageIntersect>(query, new { Consecutive = id });

            foreach (var item in results)
            {
                string query2 = "SELECT * FROM satelite WHERE SateliteId = @SateliteIdRef";

                var sateliteDetails = await db.QueryAsync<Satelite>(query2, new { SateliteIdRef = item.SateliteIdRef });
                var messageWithSatelite = new
                {
                    name = sateliteDetails.SingleOrDefault()?.SateliteName,
                    Coordenadas = new[] { sateliteDetails.SingleOrDefault()?.Coordenadax, sateliteDetails.SingleOrDefault()?.Coordenaday },
                    distance = item.Distance,
                    Message = item.Message,
                };

                messageDetails.Add(messageWithSatelite);
            }

            return messageDetails;
            ;
        
        }

      

        public async Task<bool> InsertMessage(MessageIntersect messageIntersect)

        {
            var db = dbConnetion();
            var sql = @"INSERT INTO messagesIntersect(messageNum, message) 
                        VALUES(@MessageNum, @Message)";
            var result = await db.ExecuteAsync(sql, new { messageIntersect.SateliteIdRef, messageIntersect.Message });
            return result > 0;
        }

        public async Task<bool> createSatelite(Satelite satelite)
        {
            var db = dbConnetion();
            var sql = @"INSERT INTO satelite(sateliteName, coordenadax, coordenaday) 
                        VALUES(@SateliteName, @Coordenadax, @Coordenaday)";
            var result = await db.ExecuteAsync(sql, new { satelite.SateliteName, satelite.Coordenadax, satelite.Coordenaday });
            return result > 0;
        }
    }
}
