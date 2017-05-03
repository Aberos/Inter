using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Local
    {
        private Cidade cidade;

        public Local()
        {
            cidade = new Cidade();
        }
        public int Id_Local { get; set; }
        public string Endereco { get; set; }
        public string Descricao_Local { get; set; }
        public string Nome { get; set; }

        public Cidade Cidade { get { return cidade; } set { this.cidade = value; } }
    }
}