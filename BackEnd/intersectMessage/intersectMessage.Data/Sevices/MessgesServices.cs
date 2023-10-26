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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace intersectMessage.Data.Sevices
{
    public class Coordenadas
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
    public class MessagesIntersetServices : IIntersectMessage
    {
        private readonly MySqlConfig _connectionString;

        public float X { get; private set; }

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
                    Coordenadas = new[] { sateliteDetails.SingleOrDefault()?.CoordinateX, sateliteDetails.SingleOrDefault()?.CoordinateY },
                    distance = item.Distance,
                    Message = item.Message,
                };

                messageDetails.Add(messageWithSatelite);
            }

            return messageDetails;
            ;
        
        }

        public async Task<object> InsertMessage(MessageIntersectAndSatelites messageIntersectAndSatelites)
        {
            var db = dbConnetion();
            int consecutive = await getConsecutive(); //obtenemos ultimo consecutivo del mensaje intersectado
            var satelites = messageIntersectAndSatelites.Satelites;
            var message = messageIntersectAndSatelites.Messages;

            string[] messageDecrypted = new string[15];
            string messageEncrypted = "";
            

            for (int i = 0; i < message.Count; i++)
            {
                message[i].Consecutive = consecutive;
                message[i].SateliteIdRef = satelites[i].SateliteId;
                int e = 0;

                //Encriptamos el mensaje en una variable "messageEncrypted", y a la vez lo desencriptamos en la variable "messageDecrypted"
                //Esto para poder llevar un registro de los mensajes tanto encriptados como desencriptados
                foreach (var world in message[i].MessageEncrypted)
                {
                       
                    if (world != " ")
                    {
                        messageDecrypted[e] = world.ToString();
                        messageEncrypted = messageEncrypted + " " + world.ToString();
                    }
                    else 
                    {
                        messageEncrypted = messageEncrypted + " " + "''";
                    }    
                    e = e + 1; 
                }
                e = 0;
                if (messageEncrypted != "") 
                {
                    message[i].Message = messageEncrypted;
                    messageEncrypted = "";
                }

               //Guarda el mensaje encriptado que corresponde a cada satelite y se referencia por el consecutivo
                var sql = @"INSERT INTO messagesIntersect(sateliteIdRef, consecutive, distance , Message) 
                        VALUES(@SateliteIdRef, @Consecutive, @Distance, @Message)";
                await db.ExecuteAsync(sql, new { message[i].SateliteIdRef, message[i].Consecutive, message[i].Distance, message[i].Message });

            }

            //Asignamos el mensaje desencriptado a una variable tipo string, ya que el mensaje se encuentra en un array
            string messageIntersected = string.Join(" ", messageDecrypted.Where(s => !string.IsNullOrEmpty(s)));

            //Obtenemos las cordenadas del emisor del mensaje de acuerdo a la distacia y las cordenadas de los satelites;
            Coordenadas getCoordinateXY = await getCoordinates(messageIntersectAndSatelites);
            float coordinateX = getCoordinateXY.X;
            float coordinateY = getCoordinateXY.Y;

            //Ejecutamos la funcion la cual va a guardar el mensaje ya desencriptado 
            messageAndCoordinateIntersected(consecutive, coordinateX, coordinateY, messageIntersected);


            return new { messageIntersected };
        }

        public async Task<bool> messageAndCoordinateIntersected(int consecutive, float coordinateX, float coordinateY, string message)
        {
            var db = dbConnetion();
            var sql = @"INSERT INTO decryptedMessage(consecutive, coordinateX, coordinateY , Message) 
                        VALUES(@Consecutive, @CoordinateX, @CoordinateY, @Message)";
            await db.ExecuteAsync(sql, new { consecutive, coordinateX, coordinateY, message });


            return true;
        }


        public async Task<Coordenadas> getCoordinates(MessageIntersectAndSatelites messageIntersectAndSatelites)

        {
            var messages = messageIntersectAndSatelites.Messages;
            var satelites = messageIntersectAndSatelites.Satelites;

            var satelite1 = new Satelite { };
            var satelite2 = new Satelite { };
            var satelite3 = new Satelite { };

            float d1 = messages[0].Distance, d2 = messages[1].Distance, d3 = messages[2].Distance;

            //Se extrae la información de los satélites y se guarda en las diferentes variables.
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

            //Calcular coeficientes para el sistema de ecuaciones
            float A = -2 * (satelite1.CoordinateX + 2 * satelite2.CoordinateX);
            float B = -2 * (satelite1.CoordinateY + 2 * satelite2.CoordinateY);

            float C = d1 * d1 - d3 * d3 - satelite1.CoordinateX * satelite1.CoordinateX + satelite2.CoordinateX * satelite2.CoordinateX - satelite1.CoordinateY * satelite1.CoordinateY + satelite2.CoordinateY * satelite2.CoordinateY;

            float D = -2 * (satelite2.CoordinateX + 2 * satelite3.CoordinateX);
            float E = -2 * (satelite2.CoordinateY + 2 * satelite3.CoordinateY);

            float F = d1 * d1 - d2 * d2 - satelite2.CoordinateX * satelite2.CoordinateX + satelite3.CoordinateX * satelite3.CoordinateX - satelite2.CoordinateY * satelite2.CoordinateY + satelite3.CoordinateY * satelite3.CoordinateY;

            //Resolver el sistema de ecuaciones
            float Z = A * E - B * D;

            float DX = C * E - B * F;
            float DY = A * F - C * D;

            float x = (float)Math.Round(DX / Z, 2);
            float y = (float)Math.Round(DY / Z, 2);

            return new Coordenadas { X = x, Y = y };
        }

        public async Task<int> getConsecutive()
        {
            var db = dbConnetion();
            string query = "SELECT MAX(Consecutive) FROM decryptedMessage";

            int maxConsecutive = await db.ExecuteScalarAsync<int>(query);
            maxConsecutive = maxConsecutive + 1;

            return maxConsecutive;
        }

        public async Task<bool> createSatelite(Satelite satelite)
        {
            var db = dbConnetion();
            var sql = @"INSERT INTO satelite(sateliteName, coordenadax, coordenaday) 
                        VALUES(@SateliteName, @Coordenadax, @Coordenaday)";
            var result = await db.ExecuteAsync(sql, new { satelite.SateliteName, satelite.CoordinateX, satelite.CoordinateY });
            return result > 0;
        }

        public async Task<IEnumerable<Satelite>> GetAllSatelites()
        {
            var db = dbConnetion();
            var sql = @"SELECT sateliteId, sateliteName, CoordinateX, CoordinateY
                        FROM satelite";
            return await db.QueryAsync<Satelite>(sql, new { });
        }

        public async Task<IEnumerable<DecryptedMessage>> GetAllMessagesIntersected()
        {
            var db = dbConnetion();
            var sql = @"SELECT Consecutive, CoordinateX, CoordinateY, Message
                        FROM decryptedMessage";
            return await db.QueryAsync<DecryptedMessage>(sql, new { });
        }


    }
}
