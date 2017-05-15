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
        public Evento()
        {
            esporte = new Esporte();
            local = new Local();
            organizador = new Organizador();
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Id_Evento { get; set; }
        public int Estatus { get; set; }
        public int MaxJogadores { get; set; }

        public string Horario { get; set; }
        public string Data { get; set; }

        public string Imagem { get; set; }

        public Esporte Esporte { get { return esporte; } set { this.esporte = value; } }
        public Local Local { get { return local; } set { this.local = value; } }
        public Organizador Organizador { get { return organizador; } set { this.organizador = value; } }

    }
}