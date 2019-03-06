using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Models
{
    /// <summary>
    /// PregutaRespuesta
    /// </summary>
    public class PreguntaRespuesta
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Pregunta
        /// </summary>
        public string Pregunta { get; set; }
    }
    /// <summary>
    /// PreguntaAbierta
    /// </summary>
    public class PreguntaAbierta : PreguntaRespuesta
    {
        /// <summary>
        /// Respuestas
        /// </summary>
        public List<string> Respuestas { get; set; }
    }
    /// <summary>
    /// PreguntaEleccionUnica
    /// </summary>
    public class PreguntaEleccionUnica : PreguntaRespuesta
    {
        /// <summary>
        /// Respuesta
        /// </summary>
        public List<string> Respuestas { get; set; }
    }
}
