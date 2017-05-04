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
            string strConn = "Data Source = DESKTOP-3BNB4EI\\SQLEXPRESS; Initial Catalog = isports; Integrated Security = true";
            connection = new SqlConnection(strConn);

            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}