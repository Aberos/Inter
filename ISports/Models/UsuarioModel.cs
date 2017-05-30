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
            cmd.CommandText = @"Exec CadUsuario 1, @email, @nome, @sobreNome, @dataNasc, @cel, @senha, '/Pictures/user_profile.jpg'" ;

            cmd.Parameters.AddWithValue("@email", c.Email);
            cmd.Parameters.AddWithValue("@nome", c.Nome);
            cmd.Parameters.AddWithValue("@sobreNome", c.Sobrenome);
            cmd.Parameters.AddWithValue("@dataNasc", c.DataNasc);
            cmd.Parameters.AddWithValue("@cel", c.Cel);
            cmd.Parameters.AddWithValue("@senha", c.Senha);
            
            cmd.ExecuteNonQuery();
        }

        public Usuario Read(string email, string senha)
        {
            Usuario e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Exec Logar @email, @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                e = new Usuario();
                e.Id_usuario = (int)reader["id"];
                e.Nome = (string)reader["nome"];
                e.Email = (string)reader["email"];
                e.Foto_Perfil = (string)reader["foto_perfil"];
            }
            return e;
        }
    }
}