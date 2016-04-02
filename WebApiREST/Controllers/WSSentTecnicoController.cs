using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiREST.BusinessLogic;
using WebApiREST.BusinessEntities.Interface;

namespace WebApiREST.Controllers
{
    public class WSSentTecnicoController : ApiController
    {
        private readonly ITecnicoBusinessLogic oTecnicoBusinessLogic;

        public WSSentTecnicoController(ITecnicoBusinessLogic oTecnicoBusinessLogic)
        {
            this.oTecnicoBusinessLogic = oTecnicoBusinessLogic;
        }

        [Route("api/WSSentTecnico/{Nombre}")]
        [HttpGet]
        public IHttpActionResult ObtenerTecnico([FromUri] string nombre)
        {
            nombre = (nombre == "laralalalala") ? string.Empty : nombre;
            DTOTecnicoRespuesta DTOTecnicoRespuesta = oTecnicoBusinessLogic.ObtenerTecnico(nombre);
            return Ok(DTOTecnicoRespuesta);
        }

        public IHttpActionResult Post()
        {
            //REGISTRAR
            return Ok("CORRECTO");
        }

    }
}