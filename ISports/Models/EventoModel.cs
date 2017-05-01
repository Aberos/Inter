using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class EventoModel : ConexaoBase
    {
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
            cmd.Parameters.AddWithValue("@idCidade", e.Cidade.codigo);

            cmd.ExecuteNonQuery();

        }


        //Buscar Evento Por Cidade Estado e Esporte
         public List<Evento> Search(string Cidade , string Estado, int IdEsporte)
        {
            List<Evento> lista = new List<Evento>();



            return lista;
        }

        //Ler Evento pelo id do Evento
        public Evento Read(int IdEvento)
        {
            Evento e = null;



            return e;
        }

        //Retornar Eventos que o Usuario Está Organizando
        public List<Evento> MyEvents(int IdOrganizador)
        {
            List<Evento> lista = new List<Evento>();



            return lista;
        }

        //
        public List<Evento> FeedEvents(int IdUsuario)
        {
            List<Evento> lista = new List<Evento>();



            return lista;
        }
    }
}