using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ISports.Models
{
    public class ConexaoBase : IDisposable
    {
        protected SqlConnection connection;

        public ConexaoBase()
        {
            string strConn = "Data Source = localhost; Initial Catalog = isports; Integrated Security = true; MultipleActiveResultSets = true";
            connection = new SqlConnection(strConn);

            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}