using Dapper;
using Google.Protobuf.WellKnownTypes;
using intersectMessage.Data.Interfaces;
using intersectMessage.Data.Models;
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

      

        public async Task<Object> InsertMessage(MessageIntersectAndSatelites messageIntersectAndSatelites)

        {
            var messages = messageIntersectAndSatelites.Messages;
            var satelites = messageIntersectAndSatelites.Satelites;

            var satelite1 = new Satelite { };
            var satelite2 = new Satelite { };
            var satelite3 = new Satelite { };

            float d1 = messages[0].Distance, d2 = messages[1].Distance, d3 = messages[2].Distance;


            for (int i = 0; i < satelites.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        satelite1 = satelites[i];
                        d1 = messages[i].Distance;
   
                        break;
                    case 1:
                        satelite2 = satelites[i];
                        d2 = messages[i].Distance;
                        break;
                    case 2:
                        satelite3 = satelites[i];
                        d3 = messages[i].Distance;
                        break;
                    default:
       
                        break;
                }     
            }

            float A = -2 * (satelite1.Coordenadax + 2 * satelite2.Coordenadax);
            float B = -2 * (satelite1.Coordenaday + 2 * satelite2.Coordenaday);

            float C = d1 * d1 - d3 * d3 - satelite1.Coordenadax * satelite1.Coordenadax + satelite2.Coordenadax * satelite2.Coordenadax - satelite1.Coordenaday * satelite1.Coordenaday + satelite2.Coordenaday * satelite2.Coordenaday;

            float D = -2 * (satelite2.Coordenadax + 2 * satelite3.Coordenadax);
            float E = -2 * (satelite2.Coordenaday + 2 * satelite3.Coordenaday);

            float F = d1 * d1 - d2 * d2 - satelite2.Coordenadax * satelite2.Coordenadax + satelite3.Coordenadax * satelite3.Coordenadax - satelite2.Coordenaday * satelite2.Coordenaday + satelite3.Coordenaday * satelite3.Coordenaday;

            float Z = A * E - B * D;

            float DX = C * E - B * F;
            float DY = A * F - C * D;

            float x = (float)Math.Round(DX / Z, 2);
            float y = (float)Math.Round(DY / Z, 2);

            var db = dbConnetion();
            //var sql = @"INSERT INTO messagesIntersect(messageNum, message) 
            //            VALUES(@MessageNum, @Message)";
            //var result = await db.ExecuteAsync(sql, new { messageIntersectAndSatelites, });
            return new { x, y };
        }

        public async Task<bool> createSatelite(Satelite satelite)
        {
            var db = dbConnetion();
            var sql = @"INSERT INTO satelite(sateliteName, coordenadax, coordenaday) 
                        VALUES(@SateliteName, @Coordenadax, @Coordenaday)";
            var result = await db.ExecuteAsync(sql, new { satelite.SateliteName, satelite.Coordenadax, satelite.Coordenaday });
            return result > 0;
        }

        public async Task<IEnumerable<Satelite>> GetAllSatelites()
        {
            var db = dbConnetion();
            var sql = @"SELECT sateliteId, sateliteName, coordenadax, coordenaday
                        FROM satelite";
            return await db.QueryAsync<Satelite>(sql, new { });
        }
    }
}
