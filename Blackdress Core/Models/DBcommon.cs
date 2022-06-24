using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace Blackdress_Core.Models
{
    public class DBcommon
    {
        private static string Conn = @"server=127.0.0.1;user id=root;persistsecurityinfo=True;database=blackdress";

        public static IDbConnection Conexion()
        {
            return new MySqlConnection(Conn);
        }
    }
}
