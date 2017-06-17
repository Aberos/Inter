using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Noticia
    {
        public int Id_Noticia { get; set; }
        public int Id_Evento { get; set; }
        public string Descricao_Noticia { get; set; }

        public DateTime Data_Noticia { get; set; }
    }
}