using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class CidadeModel : ConexaoBase
    {
        public List<Cidade> Cidades(string estado)
        {
            List<Cidade> lista = new List<Cidade>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from cidades where estado = @estado";

            cmd.Parameters.AddWithValue("@estado", estado);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cidade c = new Cidade();
                c.codigo = (int)reader["cod"];
                c.Nome = (string)reader["nome"];
                c.Uf.Sigla = (string)reader["estado"];
                lista.Add(c);
            }

            return lista;

        }
    }
}