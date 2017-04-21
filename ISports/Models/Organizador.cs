using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Organizador : Usuario
    {
        public int Permissao { get; set; }
        public double Qualificacao { get; set; }

    }
}