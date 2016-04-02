using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ApiService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class PlanificacionSRV : IPlanificacionSRV
    {
        string conexion = "Data Source = (local); Initial Catalog = ABELINBD; Integrated Security = SSPI";

        public List<Planificaciones> ObtenerPlanificaciones(Planificaciones oFiltro)
        {
            List<Planificaciones> oPlanificaciones = new List<Planificaciones>();
            Planificaciones oPlanificacion;
            StringBuilder strCommand = new StringBuilder();
            bool needConector = false;
            string conector;

            strCommand.Append("SELECT P.[Codigo], P.[Contrato], P.[Visita], P.[Trabajo], C.[RazonSocial], Ct.[Inicio], Ct.[Fin], T.[Nombre] ");
            strCommand.Append("FROM [Planificaciones] ");
            strCommand.Append("INNER JOIN [Contratos] Ct ON Ct.Codigo = P.Contrato ");
            strCommand.Append("INNER JOIN [Clientes] C ON C.Codigo = Ct.Cliente ");
            strCommand.Append("INNER JOIN [Tecnicos] T ON T.Codigo = Ct.Tecnico ");

            if (!string.IsNullOrEmpty(oFiltro.Codigo))
            {
                conector = (needConector) ? "AND " : "WHERE ";
                strCommand.Append(conector).Append("P.[Codigo] = @Codigo ");
            }
            if (!string.IsNullOrEmpty(oFiltro.CodigoContrato))
            {
                conector = (needConector) ? "AND " : "WHERE ";
                strCommand.Append(conector).Append("P.[Contrato] = @Contrato ");
            }
            if (oFiltro.Visita != null)
            {
                conector = (needConector) ? "AND " : "WHERE ";
                strCommand.Append(conector).Append("P.[Visita] = @Visita ");
            }
            if (oFiltro.Trabajo != null)
            {
                conector = (needConector) ? "AND " : "WHERE ";
                strCommand.Append(conector).Append("P.[Trabajo] = @Trabajo ");
            }
            //if (oFiltro. != null)
            //{
            //    conector = (needConector) ? "AND " : "WHERE ";
            //    strCommand.Append(conector).Append("P.[RazonSocial] = @RazonSocial ");
            //}
            //if (oFiltro.Trabajo != null)
            //{
            //    conector = (needConector) ? "AND " : "WHERE ";
            //    strCommand.Append(conector).Append("P.[Trabajo] = @Trabajo ");
            //}


            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(strCommand.ToString(), con))
                {
                    com.Parameters.Add(new SqlParameter("@Codigo", oFiltro.Codigo));
                    com.Parameters.Add(new SqlParameter("@Contrato", oFiltro.Contrato));
                    com.Parameters.Add(new SqlParameter("@Visita", oFiltro.Visita));
                    com.Parameters.Add(new SqlParameter("@Trabajo", oFiltro.Trabajo));

                    using (SqlDataReader resultado = com.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            oPlanificacion = new Planificaciones();
                            oPlanificacion.Contrato = new Contratos();
                            oPlanificacion.Contrato.Cliente = new Clientes();
                            oPlanificacion.Contrato.Tecnico = new Tecnicos();


                            oPlanificacion.Codigo = (string)resultado["Codigo"];
                            oPlanificacion.CodigoContrato = (string)resultado["Contrato"];
                            oPlanificacion.Visita = (DateTime)resultado["Visita"];
                            oPlanificacion.CodigoTrabajo = (string)resultado["Trabajo"];

                            oPlanificacion.Contrato.Inicio = (DateTime)resultado["Inicio"];
                            oPlanificacion.Contrato.Fin = (DateTime)resultado["Fin"];
                            oPlanificacion.Contrato.Tecnico.Nombre = (string)resultado["Nombre"];
                        }
                    }
                }
            }

            return oPlanificaciones;
        }

        Planificaciones IPlanificacionSRV.ObtenerPlanificaciones(Planificaciones oFiltro)
        {
            throw new NotImplementedException();
        }
    }
}
