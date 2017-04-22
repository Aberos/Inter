using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Usuario
    {
        public int Id_usuario { get; set; }

        public int Estatus { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public DateTime DataNasc { get; set; }

        public string Cel { get; set; }

        public string Senha { get; set; }

        public byte[] Foto_Perfil { get; set; }

    }
}