using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Contratos
    {
        public string Codigo { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoTecnico { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public String InicioString { get; set; }
        public String FinString { get; set; }
        public string Estado { get; set; }

        public Clientes Cliente { get; set; }
        public Tecnicos Tecnico { get; set; }
    }
}
