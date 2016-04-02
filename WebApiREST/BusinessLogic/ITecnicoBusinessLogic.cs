using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiREST.BusinessEntities.Interface;

namespace WebApiREST.BusinessLogic
{
    public interface ITecnicoBusinessLogic
    {
        DTOTecnicoRespuesta ObtenerTecnico(string nombre);
    }
}