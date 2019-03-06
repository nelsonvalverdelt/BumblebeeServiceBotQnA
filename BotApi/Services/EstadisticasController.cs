using BotApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.Services
{
    /// <summary>
    /// EstadisticasController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        /// <summary>
        /// Registrar pregunta abierta
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="500">Server Error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost("pregunta/abierta/{respuesta}")]
        public async Task<IActionResult> PostPreguntaAbierta(string respuesta)
        {
            try
            {
                var lista = await LeerPreguntaAbierta();
                var respuestas = lista.Respuestas;
                respuestas.Add(respuesta);
                await GuardarJson("./Services/Data/PreguntaAbierta.json", lista);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Registrar pregunta de elección única
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="500">Server Error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost("pregunta/eleccion/unica/{respuesta}")]
        public async Task<IActionResult> PostPreguntaEleccionUnica(string respuesta)
        {
            try
            {
                var lista = await LeerPreguntaEleccionUnica();
                var respuestas = lista.Respuestas;
                respuestas.Add(respuesta);
                await GuardarJson("./Services/Data/PreguntaEleccionUnica.json", lista);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lista de preguntas abiertas
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="500">Server Error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("pregunta/abierta")]
        public async Task<IActionResult> GetPreguntaAbierta()
        {
            try
            {
                var lista = await LeerPreguntaAbierta();
                var respuestas = lista.Respuestas;
                return Ok(respuestas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lista de preguntas Elección única
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="500">Server Error</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("pregunta/eleccion/unica")]
        public async Task<IActionResult> GetPreguntaEleccionUnica()
        {
            try
            {
                var lista = await LeerPreguntaEleccionUnica();
                var respuestas = lista.Respuestas;
                return Ok(respuestas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #region Metodos privados

#pragma warning disable CS1591

        public async static Task<PreguntaAbierta> LeerPreguntaAbierta()
        {
            var resultado = await LeerJson<PreguntaAbierta>("./Services/Data/PreguntaAbierta.json");
            return resultado;
        }

        public async static Task<PreguntaEleccionUnica> LeerPreguntaEleccionUnica()
        {
            var resultado = await LeerJson<PreguntaEleccionUnica>("./Services/Data/PreguntaEleccionUnica.json");
            return resultado;
        }

        private async static Task<TResult> LeerJson<TResult>(string filePath)
        {
            //Encoding.GetEncoding("iso-8859-1"): Lee caracteres especiales

            using (var r = new StreamReader(filePath, Encoding.GetEncoding("iso-8859-1")))
            {
                var readJson = await r.ReadToEndAsync();
                var result = JsonConvert.DeserializeObject<TResult>(readJson);
                return result;
            }
        }

        private async static Task GuardarJson<TObject>(string filePath, TObject data)
        {
            var json = JsonConvert.SerializeObject(data);
            await System.IO.File.WriteAllTextAsync(filePath, json, Encoding.GetEncoding("iso-8859-1"));
        }

#pragma warning restore CS1591

        #endregion Metodos privados
    }
}