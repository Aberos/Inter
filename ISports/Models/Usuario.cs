using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISports.Models
{
    public class Usuario
    {
        
        public int Id_usuario { get; set; }

        public int Estatus { get; set; }
        
        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string Nome { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string Sobrenome { get; set; }

        [Required]
        [DisplayName("Birth Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc { get; set; }

        [Required]
        [DisplayName("Cellphone")]
        public string Cel { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Senha { get; set; }

        public string Foto_Perfil { get; set; }

        public int InscricaoStatus { get; set; }

    }
}