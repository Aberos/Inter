using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Pagamento
    {
        public int Id_Evento { get; set; }
        public int Id_Usuario { get; set; }
        public string Data_Pagamento { get; set; }
        public double Valor { get; set; }
        public int Tipo_Pgmt { get; set; }
    }
}