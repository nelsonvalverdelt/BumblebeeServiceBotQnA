using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Models
{
    /// <summary>
    /// Usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// Telefono
        /// </summary>
        public string Telefono { get; set; }
        /// <summary>
        /// Celular
        /// </summary>
        public string Celular { get; set; }
        /// <summary>
        /// Dni
        /// </summary>
        public string Dni { get; set; }
        /// <summary>
        /// Codigo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Contrasena
        /// </summary>
        public string Contrasena { get; set; }
    }
    /// <summary>
    /// Cuenta
    /// </summary>
    public class Cuenta
    {
        /// <summary>
        /// Codigo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Contrasena
        /// </summary>
        public string Contrasena { get; set; }
    }
}
