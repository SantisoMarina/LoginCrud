using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGMCLIP.ViewModels
{
    public class PersonaVM
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
        [Required]
        public string email { get; set; }
        /// <summary>
        /// El id de la provincia de la persona
        /// </summary>
        public string id_provincia { get; set; }
        /// <summary>
        /// La provincia de la persona
        /// </summary>
        public string provincia { get; set; }
        /// <summary>
        /// El usuario de la persona
        /// </summary>
        [Required]
        public string usuario { get; set; }

    }
}