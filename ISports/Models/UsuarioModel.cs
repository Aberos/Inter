using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ISports.Models
{
    public class UsuarioModel : ConexaoBase
    {
        public void Create(Usuario c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Exec CadUsuario 1, @email, @nome, @sobreNome, @dataNasc, @cel, @senha, '~/Pictures/background.jpg'" ;

            cmd.Parameters.AddWithValue("@email", c.Email);
            cmd.Parameters.AddWithValue("@nome", c.Nome);
            cmd.Parameters.AddWithValue("@sobreNome", c.Sobrenome);
            cmd.Parameters.AddWithValue("@dataNasc", c.DataNasc);
            cmd.Parameters.AddWithValue("@cel", c.Cel);
            cmd.Parameters.AddWithValue("@senha", c.Senha);
            
            cmd.ExecuteNonQuery();
        }
    }
}