using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class OrganizadorModel : ConexaoBase
    {
        public decimal getMediaOrganizador(int idOrg)
        {
            decimal m = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select AVG(v.Nota) MEDIA from v_usuario_evento v, eventos e where v.IdEvento = e.id and e.id_organizador = @idOrg";

            cmd.Parameters.AddWithValue("@idOrg", idOrg);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                m = (int)reader["MEDIA"];
            }

            return m;
        }


        public void UpdateQualificacaoOrg(int idOrg)
        {
            decimal n = 0;
            using (OrganizadorModel model = new OrganizadorModel())
            {
                n = model.getMediaOrganizador(idOrg);
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = @"UPDATE organizadores set qualificacao = @media where id_usuario = @idOrg";

            cmd.Parameters.AddWithValue("@idOrg", idOrg);
            cmd.Parameters.AddWithValue("@media", n);

            cmd.ExecuteNonQuery();

        }
    }
}