using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SPMAA.Controllers
{
    public class PlanificacionController : Controller
    {
        private string conexion = "Data Source=(local); Initial Catalog = ABELINBD; Integrated Security = SSPI; ";

        //
        // GET: /Planificacion/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetListadoPlanificaciones(string contrato, string tecnico)
        {
            List<Planificaciones> oListaPlanificaciones = new List<Planificaciones>();
            Planificaciones oPlanificaciones = null;
            bool needConnector = false;
            string connector;

            SqlConnection con = new SqlConnection(conexion);
            con.Open();

            StringBuilder str = new StringBuilder();
            str.Append("SELECT C.Codigo, C.Fin, T.Nombre, C.Estado ");
            str.Append("FROM Planificaciones P ");
            str.Append("INNER JOIN Contratos C ON C.Codigo = P.Contrato ");
            str.Append("INNER JOIN Tecnicos T ON T.Codigo = C.Tecnico ");
            if (!String.IsNullOrEmpty(contrato))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("C.Codigo = @Contrato ");
            }
            if (!String.IsNullOrEmpty(tecnico))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("T.Nombre = @Tecnico ");
            }

            using (SqlCommand com = new SqlCommand(str.ToString(), con))
            {
                if (!String.IsNullOrEmpty(contrato))
                    com.Parameters.Add(new SqlParameter("@Contrato", contrato));
                if (!String.IsNullOrEmpty(tecnico))
                    com.Parameters.Add(new SqlParameter("@Tecnico", tecnico));

                using (SqlDataReader resp = com.ExecuteReader())
                {
                    while (resp.Read())
                    {
                        oPlanificaciones = new Planificaciones();
                        oPlanificaciones.Contrato = new Contratos();
                        oPlanificaciones.Contrato.Tecnico = new Tecnicos();

                        oPlanificaciones.Contrato.Codigo = Convert.ToString(resp["Codigo"]);
                        oPlanificaciones.Contrato.Fin = Convert.ToDateTime(resp["Fin"]);
                        oPlanificaciones.Contrato.Estado = Convert.ToString(resp["Estado"]);
                        oPlanificaciones.Contrato.Tecnico.Nombre = Convert.ToString(resp["Nombre"]);

                        oListaPlanificaciones.Add(oPlanificaciones);
                    }
                }
            }

            var jWriter = JsonConvert.SerializeObject(oListaPlanificaciones);
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
