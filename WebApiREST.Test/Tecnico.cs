using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiREST.Test
{
    public class Tecnico
    {
        public DTOHeader DTOHeader { get; set; }
        public List<DTOTecnico> DTOTecnicoLista { get; set; }
    }
    public class DTOTecnico
    {
        public int CantidadContratos { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
    public class DTOHeader
    {
        public string CodigoRetorno { get; set; }
        public string DescRetorno { get; set; }
    }
}