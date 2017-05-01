using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Evento
    {
        private Esporte esporte;
        private Local local;
        private Organizador organizador;
        private Cidade cidade;
        public Evento()
        {
            esporte = new Esporte();
            local = new Local();
            organizador = new Organizador();
            cidade = new Cidade();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Id_Evento { get; set; }
        public int Estatus { get; set; }
        public int MaxJogadores { get; set; }

        public int Horario { get; set; }
        public int Data { get; set; }

        public byte[] Imagem { get; set; }

        public Esporte Esporte { get { return esporte; } set { this.esporte = value; } }
        public Local Local { get { return local; } set { this.local = value; } }
        public Organizador Organizador { get { return organizador; } set { this.organizador = value; } }

        public Cidade Cidade { get { return cidade; } set { this.cidade = value; } }
    }
}