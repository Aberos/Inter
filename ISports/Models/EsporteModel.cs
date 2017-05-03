using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class EsporteModel: ConexaoBase
    {
        public List<Esporte> Esportes()
        {
            List<Esporte> lista = new List<Esporte>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from esportes";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Esporte e = new Esporte();
                e.Id_Esporte = (int)reader["id"];
                e.Descricao_Esporte = (string)reader["descricao"];
                lista.Add(e);
            }

            return lista;

        }
    }
}