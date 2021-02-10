using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGMCLIP.Models
{
    public class Persona
    {
        /// <summary>
        /// El id de la persona
        /// </summary>
        public int id_persona { get; set; }
        /// <summary>
        /// El nombre de la persona
        /// </summary>
        [Required]
        public string nombre { get; set; }
        /// <summary>
        /// El apellido de la persona
        /// </summary>
        [Required]
        public string apellido { get; set; }
        /// <summary>
        /// El email de la persona
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [Required]
        public string email { get; set; }
        /// <summary>
        /// La fecha de nacimiento de la persona
        /// </summary>
        [Required]
        public DateTime fecha_nacimiento { get; set; }
        /// <summary>
        /// El telefono de la persona
        /// </summary>
        [RegularExpression("([1-9][0-9]*)")]
        public int telefono { get; set; } // POR QUÉ EL NO REQUIRED NO ANDAAAAAAA
        /// <summary>
        /// La provincia asociada a la persona
        /// </summary>
        [Required]
        public int id_provincia { get; set; }

        
    }
}