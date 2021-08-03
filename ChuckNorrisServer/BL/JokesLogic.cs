using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class JokesLogic : IDisposable
    {
        public List<DAL.Models.Joke> GetJokes()
        {
            var db = DAL.Services.SqlServicesPool.CNDB;

            using (var connection = db.StartConnection())
            {
                var jokes = db.SelectAsList<DAL.Models.Joke>(connection, "spGetJokes");

                db.StopConnection(connection);

                return jokes;

            }; // here the connection is disposed!!! (free server memory)
        }

        public bool UpdateJokeLike(int id)
        {
            var db = DAL.Services.SqlServicesPool.CNDB;

            using (var connection = db.StartConnection())
            {
                var result = db.Exec(connection, "spSaveJokeLike", new SqlParameter("@Id", id));

                db.StopConnection(connection);

                return result;
            };
        }

        public void Dispose()
        {
            // any operation which aim to free memory is welcomed here :)
        }
    }
}
