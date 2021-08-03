using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class SqlServicesPool
    {
        static SqlServicesPool()
        {
            CNDB = new Services.SqlService("CNDBConnectionString");
        }

        public static Services.SqlService CNDB { get; private set; }
    }
}
