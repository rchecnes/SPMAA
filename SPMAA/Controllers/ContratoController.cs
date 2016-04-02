using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using SPMAA.ResourceAccess;

namespace SPMAA.Controllers
{
    public class ContratoController : Controller
    {
        //
        // GET: /Contrato/
        public ActionResult Index()
        {
            return View();
        }
        //GET: /asignarcontrato

        [HttpGet]
        public JsonResult GetListadoContratos(string codigo, DateTime? inicioContrato, DateTime? finContrato, string razonSocial, string estado)
        {
            List<Contratos> oListaContratos = new List<Contratos>();
            Contratos oContratos = null;
            bool needConnector = false;
            string connector;

            SqlConnection con = new SqlConnection(DABase.cnsServicioODP);
            con.Open();

            StringBuilder str = new StringBuilder();
            str.Append("SELECT Con.Codigo, Cli.RazonSocial, Con.Inicio, Con.Fin, T.Nombre, Con.Estado FROM Contratos Con ");
            str.Append("INNER JOIN Clientes Cli ON Cli.Codigo = Con.Cliente ");
            str.Append("LEFT JOIN Tecnicos T ON T.Codigo = CON.Tecnico ");
            if (!String.IsNullOrEmpty(codigo))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("Con.Codigo = @Codigo ");
            }
            if (!String.IsNullOrEmpty(razonSocial))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("Cli.RazonSocial LIKE @RazonSocial ");
            }
            if (inicioContrato != null && finContrato != null)
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("(Con.Fin BETWEEN @FechaInicio AND @FechaFin) ");
            }
            if (!String.IsNullOrEmpty(estado))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("Con.Estado = @Estado");
            }
            if (Session["rol_usuario"] == "Tecnico")
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("T.Nombre = @Nombre");
            }

            using (SqlCommand com = new SqlCommand(str.ToString(), con))
            {
                if (!String.IsNullOrEmpty(codigo))
                    com.Parameters.Add(new SqlParameter("@Codigo", codigo));
                if (!String.IsNullOrEmpty(razonSocial))
                    com.Parameters.Add(new SqlParameter("@RazonSocial", razonSocial));
                if (inicioContrato != null)
                    com.Parameters.Add(new SqlParameter("@FechaInicio", inicioContrato));
                if (finContrato != null)
                    com.Parameters.Add(new SqlParameter("@FechaFin", finContrato));
                if (!String.IsNullOrEmpty(estado))
                    com.Parameters.Add(new SqlParameter("@Estado", estado));
                if (Session["rol_usuario"] == "Tecnico")
                    com.Parameters.Add(new SqlParameter("@Nombre", Session["nombre_usuario"]));

                using (SqlDataReader resp = com.ExecuteReader())
                {
                    while (resp.Read())
                    {
                        oContratos = new Contratos();
                        oContratos.Cliente = new Clientes();
                        oContratos.Tecnico = new Tecnicos();

                        oContratos.Codigo = Convert.ToString(resp["Codigo"]);
                        oContratos.Cliente.RazonSocial = Convert.ToString(resp["RazonSocial"]);
                        oContratos.Inicio = Convert.ToDateTime(resp["Inicio"]);
                        oContratos.Fin = Convert.ToDateTime(resp["Fin"]);
                        oContratos.InicioString = oContratos.Inicio.ToShortDateString();
                        oContratos.FinString = oContratos.Fin.ToShortDateString();
                        oContratos.Tecnico.Nombre = Convert.ToString(resp["Nombre"]);
                        oContratos.Estado = Convert.ToString(resp["Estado"]);

                        oListaContratos.Add(oContratos);
                    }
                }
            }

            var jWriter = JsonConvert.SerializeObject(oListaContratos);
            return Json(jWriter, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AsignarTecnico()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetListadoTecnicos(string nombre)
        {
            nombre = (string.IsNullOrEmpty(nombre)) ? "laralalalala" : nombre;
            string resultado;
            var url = "http://localhost:47475/api/WSSentTecnico/" + nombre;
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            using (var response = webrequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
                resultado = reader.ReadToEnd();
            string res = resultado.Substring(31, (resultado.IndexOf("\",\"DescRetorno\"")) - 31);
            if (res == "Correcto")
            {
                resultado = resultado.Substring(resultado.IndexOf("[{\"Codigo\":\""), resultado.Length - (resultado.IndexOf("{\"Codigo\":\"")));
            }
            else
            {
                //Manejar de alguna manera el error (mostrar una alerta o algo)
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public String grabarTecnicoContrato(string CodigoTecnico, string CodigoContrato)
        {
            //implementar el actualizar tecnico en el contrato.
            return "";
        }

        //GET: /consolidar informe
        public ActionResult ConsolidarInforme()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetListadoInforme(string codigo, string razonSocial, string contrato)
        {
            List<Informes> oListaInformes = new List<Informes>();
            Informes oInforme = null;
            bool needConnector = false;
            string connector;

            SqlConnection con = new SqlConnection(DABase.cnsServicioODP);
            con.Open();

            StringBuilder str = new StringBuilder();
            str.Append("SELECT I.Codigo, I.Creacion, I.Detalle, I.Observacion, I.Estado ");
            str.Append("FROM Informes I ");
            str.Append("INNER JOIN Contratos C ON C.Codigo = I.Contrato ");
            str.Append("INNER JOIN Clientes Cl ON Cl.Codigo = C.Cliente ");
            if (!String.IsNullOrEmpty(codigo))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("I.Codigo = @Codigo ");
            }
            if (!String.IsNullOrEmpty(razonSocial))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("Cl.RazonSocial = @RazonSocial ");
            }
            if (!String.IsNullOrEmpty(contrato))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("I.Contrato = @Contrato ");
            }

            using (SqlCommand com = new SqlCommand(str.ToString(), con))
            {
                if (!String.IsNullOrEmpty(codigo))
                    com.Parameters.Add(new SqlParameter("@Codigo", codigo));
                if (!String.IsNullOrEmpty(razonSocial))
                    com.Parameters.Add(new SqlParameter("@RazonSocial", razonSocial));
                if (!String.IsNullOrEmpty(contrato))
                    com.Parameters.Add(new SqlParameter("@Contrato", contrato));

                using (SqlDataReader resp = com.ExecuteReader())
                {
                    while (resp.Read())
                    {
                        oInforme = new Informes();
                        oInforme.Codigo = Convert.ToString(resp["Codigo"]);
                        oInforme.Creacion = Convert.ToDateTime(resp["Creacion"]);
                        oInforme.Detalle = Convert.ToString(resp["Detalle"]);
                        oInforme.Observacion = Convert.ToString(resp["Observacion"]);
                        oInforme.Estado = Convert.ToString(resp["Estado"]);

                        oListaInformes.Add(oInforme);
                    }
                }
            }

            var jWriter = JsonConvert.SerializeObject(oListaInformes);
            return Json(jWriter, JsonRequestBehavior.AllowGet);
        }


        //GET: /consolidar planificacion
        public ActionResult GenerarPlanificacion()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PostPlanificacion()
        {
            return null;
        }

        //
        // GET: /Contrato/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Contrato/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contrato/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Contrato/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contrato/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Contrato/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
