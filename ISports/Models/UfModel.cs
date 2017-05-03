using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class UfModel: ConexaoBase
    {

        public List<UF> Ufs()
        {
            List<UF> lista = new List<UF>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from ufs";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                UF u = new UF();
                u.Sigla = (string)reader["sigla"];
                u.Nome = (string)reader["nome"];
                lista.Add(u);
            }

            return lista;

        }
    }
}