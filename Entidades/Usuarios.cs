using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuarios
    {
        public string Codigo { get; set; }
        public string Password { get; set; }
        public string CodigoRol { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public Roles Rol { get; set; }
    }
}
