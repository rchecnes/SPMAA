using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Informes
    {
        public string Codigo { get; set; }
        public string CodigoContrato { get; set; }
        public DateTime? Creacion { get; set; }
        public string Detalle { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public Contratos Contrato { get; set; }
    }
}