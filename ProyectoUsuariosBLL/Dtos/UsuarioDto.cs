using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUsuariosBLL.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio.")] //Recomendable por experiencia de usuario
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Edad es obligatoria.")]
        public int? Edad { get; set; }
    }
}



