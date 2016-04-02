using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Planificaciones
    {
        public string Codigo { get; set; }
        public string CodigoContrato { get; set; }
        public DateTime Visita { get; set; }
        public string CodigoTrabajo { get; set; }

        public Contratos Contrato { get; set; }
        public Trabajos Trabajo { get; set; }
    }
}
