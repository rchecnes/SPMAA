using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SPMAA.ResourceAccess;
using SPMAA.PlanificacionesSRV;

namespace SPMAA.Controllers
{
    public class PlanificacionController : Controller
    {
        //
        // GET: /Planificacion/
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public JsonResult GetListadoPlanificaciones(string contrato, string tecnico)
        //{
        //    List<Planificaciones> oListaPlanificaciones = new List<Planificaciones>();
        //    Planificaciones oPlanificaciones = null;
        //    bool needConnector = false;
        //    string connector;

        //    SqlConnection con = new SqlConnection(DABase.cnsServicioODP);
        //    con.Open();

        //    StringBuilder str = new StringBuilder();
        //    str.Append("SELECT C.Codigo, C.Fin, T.Nombre, C.Estado ");
        //    str.Append("FROM Planificaciones P ");
        //    str.Append("INNER JOIN Contratos C ON C.Codigo = P.Contrato ");
        //    str.Append("INNER JOIN Tecnicos T ON T.Codigo = C.Tecnico ");
        //    if (!String.IsNullOrEmpty(contrato))
        //    {
        //        connector = (needConnector) ? "AND " : "WHERE ";
        //        needConnector = true;
        //        str.Append(connector).Append("C.Codigo = @Contrato ");
        //    }
        //    if (!String.IsNullOrEmpty(tecnico))
        //    {
        //        connector = (needConnector) ? "AND " : "WHERE ";
        //        needConnector = true;
        //        str.Append(connector).Append("T.Nombre = @Tecnico ");
        //    }

        //    using (SqlCommand com = new SqlCommand(str.ToString(), con))
        //    {
        //        if (!String.IsNullOrEmpty(contrato))
        //            com.Parameters.Add(new SqlParameter("@Contrato", contrato));
        //        if (!String.IsNullOrEmpty(tecnico))
        //            com.Parameters.Add(new SqlParameter("@Tecnico", tecnico));

        //        using (SqlDataReader resp = com.ExecuteReader())
        //        {
        //            while (resp.Read())
        //            {
        //                oPlanificaciones = new Planificaciones();
        //                oPlanificaciones.Contrato = new Contratos();
        //                oPlanificaciones.Contrato.Tecnico = new Tecnicos();

        //                oPlanificaciones.Contrato.Codigo = Convert.ToString(resp["Codigo"]);
        //                oPlanificaciones.Contrato.Fin = Convert.ToDateTime(resp["Fin"]);
        //                oPlanificaciones.Contrato.Estado = Convert.ToString(resp["Estado"]);
        //                oPlanificaciones.Contrato.Tecnico.Nombre = Convert.ToString(resp["Nombre"]);

        //                oListaPlanificaciones.Add(oPlanificaciones);
        //            }
        //        }
        //    }

        //    var jWriter = JsonConvert.SerializeObject(oListaPlanificaciones);
        //    return Json(jWriter, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetListadoPlanificaciones(string contrato, string tecnico)
        {
            PlanificacionClient proxy = new PlanificacionClient();

            List<Planificaciones> oListPlanificacion = new List<Planificaciones>();
            Planificaciones[] oListPlanificacionProxy;
            Planificaciones oFiltro = new Planificaciones();
            oFiltro.Contrato = new Contratos();
            oFiltro.Contrato.Codigo = contrato;
            oFiltro.Contrato.CodigoTecnico = tecnico;
            oListPlanificacionProxy = proxy.ObtenerPlanificaciones(oFiltro);

            foreach (Planificaciones oPlanificacion in oListPlanificacionProxy)
            {
                oListPlanificacion.Add(oPlanificacion);
            }

            var jWriter = JsonConvert.SerializeObject(oListPlanificacion);
            return Json(jWriter, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarPlanificacion(string contrato, DateTime visita, string trabajo)
        {
            string resultado = string.Empty;
            int ultimoCodigoPlanificaciones = 0;

            try
            {
                SqlConnection con = new SqlConnection(DABase.cnsServicioODP);
                con.Open();

                StringBuilder str = new StringBuilder();
                str.Append("SELECT TOP 1 Codigo FROM [Planificaciones] ORDER BY Codigo DESC ");

                using (SqlCommand com = new SqlCommand(str.ToString(), con))
                {
                    using (SqlDataReader resp = com.ExecuteReader())
                    {
                        if (resp.Read())
                        {
                            ultimoCodigoPlanificaciones = Convert.ToInt32(resp["Codigo"]);
                            ultimoCodigoPlanificaciones = ultimoCodigoPlanificaciones + 1;
                        }
                    }
                }

                str = new StringBuilder();
                str.Append("INSERT INTO [Planificaciones] ([Codigo], [Contrato], [Visita], [Trabajo]) ");
                str.Append("VALUES (@Codigo, @Contrato, @Visita, @Trabajo)");

                using (SqlCommand com = new SqlCommand(str.ToString(), con))
                {
                    com.Parameters.Add(new SqlParameter("@Codigo", ultimoCodigoPlanificaciones.ToString()));
                    com.Parameters.Add(new SqlParameter("@Contrato", contrato));
                    com.Parameters.Add(new SqlParameter("@Visita", visita));
                    com.Parameters.Add(new SqlParameter("@Trabajo", trabajo));
                    com.ExecuteNonQuery();
                    resultado = "Planificación registrada.";
                }
            }
            catch (System.Exception ex)
            {
                resultado = ex.Message;
            }

            var jWriter = JsonConvert.SerializeObject(resultado);
            return Json(jWriter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        //
        // GET: /Planificacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Planificacion/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Planificacion/Create
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
        // GET: /Planificacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Planificacion/Edit/5
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
        // GET: /Planificacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Planificacion/Delete/5
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
