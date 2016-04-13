using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ApiService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPlanificacion" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPlanificacion
    {
        [OperationContract]
        List<Planificaciones> ObtenerPlanificaciones(Planificaciones oFiltro);
    }
}