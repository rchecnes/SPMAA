using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;

namespace WebApiREST.BusinessEntities.Interface
{
    public class DTOTecnico : Tecnicos
    {
    }
    public class DTOTecnicoRespuesta
    {
        public DTOHeader DTOHeader { get; set; }
        public DTOTecnico DTOTecnico { get; set; }
        public List<DTOTecnico> DTOTecnicoLista { get; set; }
    }
}