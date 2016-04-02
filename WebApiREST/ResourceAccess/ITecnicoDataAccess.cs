using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiREST.BusinessEntities.Interface;

namespace WebApiREST.ResourceAccess
{
    public interface ITecnicoDataAccess
    {
        List<DTOTecnico> ObtenerTecnicos(string nombre);
    }
}