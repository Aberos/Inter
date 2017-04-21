using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Evento
    {
        public int Id_Evento { get; set; }
        public int Estatus { get; set; }
        public int MaxJogadores { get; set; }
        public int Id_Esporte { get; set; }
        public int Id_Organizador { get; set; }        
        public int Id_Local { get; set; }
    }
}