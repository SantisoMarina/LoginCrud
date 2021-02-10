using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGMCLIP.Models
{
    public class Usuario
    {
        /// <summary>
        /// El nombre de usuario
        /// </summary>
        [Required]
        public string usuario { get; set; }
        /// <summary>
        /// La contraseña del usuario
        /// </summary>
        [Required]
        public string password { get; set; }
       //no es required ya que es auto incrementable
       /// <summary>
       /// El id del usuario
       /// </summary>
        public int id_usuario { get; set; }
        /// <summary>
        /// La persona asociada al usuario
        /// </summary>
        [Required]
        public Persona persona { get; set; }
    }
}