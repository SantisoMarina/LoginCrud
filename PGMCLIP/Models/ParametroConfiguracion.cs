using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PGMCLIP.Models
{
    public class ParametroConfiguracion
    {
        /// <summary>
        /// Es el codigo del parametro
        /// </summary>
        public int codigo_parametro { get; set; }
        /// <summary>
        /// Es el nombre del parametro
        /// </summary>
        public string nombre { get; set; }
        /// <summary>
        /// Es el valor del parametro
        /// </summary>
        [Required]
        public string valor { get; set; }
        /// <summary>
        /// Indica si el parametro está habilitado o no
        /// </summary>
        [Required]
        public bool habilitado { get; set; }
    }
}