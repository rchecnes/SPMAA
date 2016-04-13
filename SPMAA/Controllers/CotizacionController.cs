using Entidades;
using Newtonsoft.Json;
using SPMAA.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;

namespace SPMAA.Controllers
{
    public class CotizacionController : Controller
    {
        //
        // GET: /Cotizacion/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetListadoConsolidarInformes(string codigo, string razonSocial, string contrato)
        {
            List<Informes> oListaInformes = new List<Informes>();
            Informes oInformes = null;
            bool needConnector = false;
            string connector;

            SqlConnection con = new SqlConnection(DABase.cnsServicioODP);
            con.Open();

            StringBuilder str = new StringBuilder();
            str.Append("SELECT I.Codigo, I.Creacion, I.Detalle, I.Observacion, I.Estado ");
            str.Append("FROM Informes I ");
            str.Append("INNER JOIN Contratos Co ON Co.Codigo = I.Contrato ");
            str.Append("INNER JOIN Clientes C ON C.Codigo = Co.Cliente ");
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
                str.Append(connector).Append("Cli.RazonSocial LIKE @RazonSocial ");
            }
            if (!String.IsNullOrEmpty(contrato))
            {
                connector = (needConnector) ? "AND " : "WHERE ";
                needConnector = true;
                str.Append(connector).Append("I.Contrato = @Contrato");
            }

            using (SqlCommand com = new SqlCommand(str.ToString(), con))
            {
                if (!String.IsNullOrEmpty(codigo))
                    com.Parameters.Add(new SqlParameter("@Codigo", codigo));
                if (!String.IsNullOrEmpty(razonSocial))
                    com.Parameters.Add(new SqlParameter("@RazonSocial", razonSocial));
                if (!String.IsNullOrEmpty(contrato))
                    com.Parameters.Add(new SqlParameter("@Estado", contrato));

                using (SqlDataReader resp = com.ExecuteReader())
                {
                    while (resp.Read())
                    {
                        oInformes = new Informes();
                        oInformes.Contrato = new Contratos();
                        oInformes.Contrato.Cliente = new Clientes();

                        oInformes.Codigo = Convert.ToString(resp["Codigo"]);
                        oInformes.Creacion = Convert.ToDateTime(resp["Creacion"]);
                        oInformes.Detalle = Convert.ToString(resp["Detalle"]);
                        oInformes.Observacion = Convert.ToString(resp["Observacion"]);
                        oInformes.Estado = Convert.ToString(resp["Estado"]);

                        oListaInformes.Add(oInformes);
                    }
                }
            }

            var jWriter = JsonConvert.SerializeObject(oListaInformes);
            return Json(jWriter, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EnviarCorreo(string codigo)
        {
            String resultado = String.Empty;
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress("devjoker@djk.com"));
            msg.From = new MailAddress("Administrador@djk.com");
            msg.Subject = "El asunto del mensaje(2.0)";
            msg.Body = "El contenido del mensaje";

            SmtpClient clienteSmtp = new SmtpClient("WIN02");

            try
            {
                clienteSmtp.Send(msg);
                resultado = "Correcto";
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
                resultado = "Incorrecto";
            }

            var jWriter = JsonConvert.SerializeObject(resultado);
            return Json(jWriter, JsonRequestBehavior.AllowGet);
        }


        // GET: /Cotizacion/nuevo
        public ActionResult Nuevo()
        {
            return View();
        }

        //
        // GET: /Cotizacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cotizacion/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cotizacion/Create
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
        // GET: /Cotizacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cotizacion/Edit/5
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
        // GET: /Cotizacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Cotizacion/Delete/5
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
