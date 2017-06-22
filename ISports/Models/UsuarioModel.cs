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
                e.Sobrenome = (string)reader["sobrenome"];
                e.DataNasc = (DateTime)reader["dataNasc"];
                e.Cel = (string)reader["cel"];
                e.Foto_Perfil = (string)reader["foto_perfil"];
            }
            return e;
        }

        public Usuario ReadId(int id)
        {
            Usuario e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from usuarios where id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                e = new Usuario();
                e.Id_usuario = (int)reader["id"];
                e.Nome = (string)reader["nome"];
                e.Email = (string)reader["email"];
                e.Sobrenome = (string)reader["sobrenome"];
                e.DataNasc = (DateTime)reader["dataNasc"];
                e.Cel = (string)reader["cel"];
                e.Foto_Perfil = (string)reader["foto_perfil"];
            }
            return e;
        }

        public void UpdateImage(int idUser, string image)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE usuarios set foto_perfil = @imagem where id = @idUsuario";

            cmd.Parameters.AddWithValue("@idUsuario", idUser);
            cmd.Parameters.AddWithValue("@imagem", image);

            cmd.ExecuteNonQuery();
        }

        public void ChangePassowrd(int idUser, string pass, string newpass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE usuarios set senha = @new where id = @idUsuario and senha = @atual";

            cmd.Parameters.AddWithValue("@idUsuario", idUser);
            cmd.Parameters.AddWithValue("@atual", pass);
            cmd.Parameters.AddWithValue("@new", newpass);

            cmd.ExecuteNonQuery();
        }

        public void EditUser(Usuario u)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE usuarios set nome = @nome, sobrenome = @sobrenome, dataNasc = @data, cel = @cel where id = @idUsuario";

            cmd.Parameters.AddWithValue("@idUsuario", u.Id_usuario);
            cmd.Parameters.AddWithValue("@nome", u.Nome);
            cmd.Parameters.AddWithValue("@sobrenome", u.Sobrenome);
            cmd.Parameters.AddWithValue("@data", u.DataNasc);
            cmd.Parameters.AddWithValue("@cel", u.Cel);

            cmd.ExecuteNonQuery();
        }
    }
}