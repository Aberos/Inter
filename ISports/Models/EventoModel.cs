﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class EventoModel : ConexaoBase
    {
        //Cadastrar Evento
         public void Create(Evento e)
         {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Exec CadEvento @nome, @descricao, @maxJogadores, @idSport, @idOrganizador, @endereco, @descricao_local, @local_nome, @dataEvento, @Horario, @imagem, @idCidade";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@descricao", e.Descricao);
            cmd.Parameters.AddWithValue("@maxJogadores", e.MaxJogadores);
            cmd.Parameters.AddWithValue("@idSport", e.Esporte.Id_Esporte);
            cmd.Parameters.AddWithValue("@idOrganizador", e.Organizador.Id_usuario);
            cmd.Parameters.AddWithValue("@descricao_local", e.Local.Descricao_Local);
            cmd.Parameters.AddWithValue("@local_nome", e.Local.Nome);
            cmd.Parameters.AddWithValue("@dataEvento", e.Data);
            cmd.Parameters.AddWithValue("@Horario", e.Horario);
            cmd.Parameters.AddWithValue("@imagem", e.Imagem);
            cmd.Parameters.AddWithValue("@idCidade", e.Local.Cidade.codigo);

            cmd.ExecuteNonQuery();

        }


        //Buscar Evento Por Cidade Estado e Esporte
        //Somente irei pegar as informações necessarias para a exibição do carousel-row
        //Quando for pra mostrar todas as informações devera ser usada o metodo Read()
        //Nesse metodo sera retornado apenas o Nome do Evento, A descricação, a foto e o Id do evento para ser utilizado no metodo Read()
        public List<Evento> Search(int idCidade , string Uf, int IdEsporte)
        {
            List<Evento> lista = new List<Evento>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_evento where CodigoCidade = @idCidade and SiglaEstado = '@Uf' and IdEsport = @IdEsporte";

            cmd.Parameters.AddWithValue("@idCidade", idCidade);
            cmd.Parameters.AddWithValue("@Uf", Uf);
            cmd.Parameters.AddWithValue("@IdEsporte", IdEsporte);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Evento e = new Evento();
                e.Id_Evento = (int)reader["Codigo"];
                e.Nome = (string)reader["NomeEvento"];
                e.Descricao = (string)reader["DescricaoEvento"];
                e.Imagem = (byte[])reader["FotoEvento"];

                lista.Add(e);
            }

            return lista;
        }

        //Ler Evento pelo id do Evento
        //Retorna Todas as informações do Evento exceeto os participantes
        public Evento Read(int IdEvento)
        {
            Evento e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_evento where Codigo = @IdEvento";

            cmd.Parameters.AddWithValue("@IdEvento", IdEvento);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                e = new Evento();
                e.Id_Evento = (int)reader["Codigo"];
                e.Estatus = (int)reader["Status"];
                e.Nome = (string)reader["NomeEvento"];
                e.Descricao = (string)reader["DescricaoEvento"];
                e.Data = (DateTime)reader["DataEvento"];
                e.Horario = (int)reader["HoraEvento"];
                e.MaxJogadores = (int)reader["MaxJogador"];
                e.Imagem = (byte[])reader["FotoEvento"];
                e.Esporte.Id_Esporte = (int)reader["IdEsport"];
                e.Esporte.Descricao_Esporte = (string)reader["NomeEsporte"];
                e.Organizador.Id_usuario = (int)reader["IdOrganizador"];
                e.Organizador.Nome = (string)reader["NomeOrganizador"];
                e.Organizador.Sobrenome = (string)reader["SobrenomeOrganizador"];
                e.Organizador.Foto_Perfil = (byte[])reader["FotoOrganizador"];
                e.Local.Id_Local = (int)reader["IdLocal"];
                e.Local.Nome = (string)reader["NomeLocal"];
                e.Local.Descricao_Local = (string)reader["NomeLocal"];
                e.Local.Endereco = (string)reader["EnderecoLocal"];
                e.Local.Cidade.codigo = (int)reader["CodigoCidade"];
                e.Local.Cidade.Nome = (string)reader["Cidade"];
                e.Local.Cidade.Uf.Sigla = (string)reader["SiglaEstado"];
                e.Local.Cidade.Uf.Nome = (string)reader["Estado"];
            }


            return e;
        }

        //Retornar Eventos que o Usuario Está Organizando
        //Somente irei pegar as informações necessarias para a exibição do carousel-row
        //Quando for pra mostrar todas as informações devera ser usada o metodo Read()
        //Nesse metodo sera retornado apenas o Nome do Evento, A descricação, a foto e o Id do evento para ser utilizado no metodo Read()
        public List<Evento> MyEvents(int IdOrganizador)
        {
            List<Evento> lista = new List<Evento>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_evento where IdOrganizador = @idOrganizador";

            cmd.Parameters.AddWithValue("@idOrganizador", IdOrganizador);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Evento e = new Evento();
                e.Id_Evento = (int)reader["Codigo"];
                e.Nome = (string)reader["NomeEvento"];
                e.Descricao = (string)reader["DescricaoEvento"];
                e.Imagem = (byte[])reader["FotoEvento"];

                lista.Add(e);
            }

            return lista;
        }

        //Retorna Evento que o Usuario está Inscrito
        //Somente irei pegar as informações necessarias para a exibição do carousel-row
        //Quando for pra mostrar todas as informações devera ser usada o metodo Read()
        //Nesse metodo sera retornado apenas o Nome do Evento, A descricação, a foto e o Id do evento para ser utilizado no metodo Read()
        public List<Evento> FeedEvents(int IdUsuario)
        {
            List<Evento> lista = new List<Evento>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_usuario_evento where IdUsuario = @idUsuario";

            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);

            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                Evento e = new Evento();
                e.Id_Evento = (int)reader["IdEvento"];
                e.Nome = (string)reader["NomeEvento"];
                e.Descricao = (string)reader["DescricaoEvento"];
                e.Imagem = (byte[])reader["DescricaoEvento"];
                lista.Add(e);
            }

            return lista;
        }

        public List<Usuario> InscritosEvento(int IdEvento)
        {
            List<Usuario> lista = new List<Usuario>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_usuario_evento where idEvento = @idEvento";

            cmd.Parameters.AddWithValue("@idEvento", IdEvento);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Usuario u = new Usuario();
                u.Id_usuario = (int)reader["IdUsuario"];
                u.Nome = (string)reader["NomeUsuario"];
                u.Sobrenome = (string)reader["SobreNomeUsuario"];
                u.DataNasc = (DateTime)reader["DataNascUsuario"];
                u.Cel = (string)reader["TelefoneUsuario"];
                u.Email = (string)reader["EmailUsuario"];
                u.Foto_Perfil = (byte[])reader["FotoUsuario"];
                lista.Add(u);
            }

            return lista;
        }

        public List<Noticia> NoticiasEvento(int IdEvento)
        {
            List<Noticia> lista = new List<Noticia>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from noticias where id_evento = @idEvento";

            cmd.Parameters.AddWithValue("@idEvento", IdEvento);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Noticia n = new Noticia();
                n.Id_Noticia = (int)reader["id"];
                n.Id_Evento = (int)reader["id_evento"];
                n.Descricao_Noticia = (string)reader["descricao"];
                lista.Add(n);

            }

            return lista;
        }
    }
}