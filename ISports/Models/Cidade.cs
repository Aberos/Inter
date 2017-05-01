using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Cidade
    {
        private UF uf;
        public Cidade()
        {
            uf = new UF();
        }
        public int codigo { get; set; }
        public string Nome { get; set; }
        public UF Uf { get { return uf; } set { this.uf = value; } }
    }
}