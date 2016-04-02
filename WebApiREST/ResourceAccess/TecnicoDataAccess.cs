using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebApiREST.BusinessEntities.Interface;

namespace WebApiREST.ResourceAccess
{
    public class TecnicoDataAccess : DABase, ITecnicoDataAccess
    {
        public List<DTOTecnico> ObtenerTecnicos(string nombre)
        {
            List<DTOTecnico> oListaTecnicos = new List<DTOTecnico>();
            DTOTecnico oTecnico = null;

            SqlConnection con = new SqlConnection(cnsServicioODP);
            con.Open();

            StringBuilder str = new StringBuilder();
            str.Append("SELECT T.Codigo, T.Nombre, COUNT(C.Tecnico) ContratosAsignados ");
            str.Append("FROM Tecnicos T ");
            str.Append("INNER JOIN Contratos C ON C.Tecnico = T.Codigo ");
            if (!String.IsNullOrEmpty(nombre))
                str.Append("WHERE T.Nombre LIKE @Nombre ");
            str.Append("GROUP BY T.Codigo, T.Nombre");

            using (SqlCommand com = new SqlCommand(str.ToString(), con))
            {
                if (!String.IsNullOrEmpty(nombre))
                    com.Parameters.Add(new SqlParameter("@Nombre", "%" + nombre + "%"));

                using (SqlDataReader resp = com.ExecuteReader())
                {
                    while (resp.Read())
                    {
                        oTecnico = new DTOTecnico();
                        oTecnico.Codigo = Convert.ToString(resp["Codigo"]);
                        oTecnico.Nombre = Convert.ToString(resp["Nombre"]);
                        oTecnico.CantidadContratos = Convert.ToInt32(resp["ContratosAsignados"]);

                        oListaTecnicos.Add(oTecnico);
                    }
                }
            }

            return oListaTecnicos;
        }
    }
}