using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ApiService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Planificacion" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Planificacion.svc o Planificacion.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Planificacion : IPlanificacion
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
            strCommand.Append("FROM [Planificaciones] P ");
            strCommand.Append("INNER JOIN [Contratos] Ct ON Ct.Codigo = P.Contrato ");
            strCommand.Append("INNER JOIN [Clientes] C ON C.Codigo = Ct.Cliente ");
            strCommand.Append("INNER JOIN [Tecnicos] T ON T.Codigo = Ct.Tecnico ");

            if (!string.IsNullOrEmpty(oFiltro.Contrato.Codigo))
            {
                conector = (needConector) ? "AND " : "WHERE ";
                strCommand.Append(conector).Append("Ct.[Codigo] = @Contrato ");
            }
            if (!string.IsNullOrEmpty(oFiltro.Contrato.CodigoTecnico))
            {
                conector = (needConector) ? "AND " : "WHERE ";
                strCommand.Append(conector).Append("T.[Codigo] = @Tecnico ");
            }

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(strCommand.ToString(), con))
                {

                    if (!string.IsNullOrEmpty(oFiltro.Contrato.Codigo)) com.Parameters.Add(new SqlParameter("@Contrato", oFiltro.Contrato.Codigo));
                    if (!string.IsNullOrEmpty(oFiltro.Contrato.CodigoTecnico)) com.Parameters.Add(new SqlParameter("@Tecnico", oFiltro.Contrato.CodigoTecnico));

                    using (SqlDataReader resultado = com.ExecuteReader())
                    {
                        while (resultado.Read())
                        {
                            oPlanificacion = new Planificaciones();
                            oPlanificacion.Contrato = new Contratos();
                            oPlanificacion.Contrato.Cliente = new Clientes();
                            oPlanificacion.Contrato.Tecnico = new Tecnicos();


                            if (!Convert.IsDBNull(resultado["Codigo"])) oPlanificacion.Codigo = (string)resultado["Codigo"];
                            if (!Convert.IsDBNull(resultado["Contrato"])) oPlanificacion.CodigoContrato = (string)resultado["Contrato"];
                            if (!Convert.IsDBNull(resultado["Visita"])) oPlanificacion.Visita = (DateTime)resultado["Visita"];
                            if (!Convert.IsDBNull(resultado["Trabajo"])) oPlanificacion.CodigoTrabajo = (string)resultado["Trabajo"];

                            if (!Convert.IsDBNull(resultado["Inicio"])) oPlanificacion.Contrato.Inicio = (DateTime)resultado["Inicio"];
                            if (!Convert.IsDBNull(resultado["Fin"])) oPlanificacion.Contrato.Fin = (DateTime)resultado["Fin"];
                            if (!Convert.IsDBNull(resultado["RazonSocial"])) oPlanificacion.Contrato.Cliente.RazonSocial = (string)resultado["RazonSocial"];
                            if (!Convert.IsDBNull(resultado["Nombre"])) oPlanificacion.Contrato.Tecnico.Nombre = (string)resultado["Nombre"];

                            oPlanificaciones.Add(oPlanificacion);
                        }
                    }
                }
            }

            return oPlanificaciones;
        }
    }
}
