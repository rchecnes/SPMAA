using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using System.Data.SqlClient;


namespace SPMAA.Controllers
{
    public class LoginController : Controller
    {
        private string conexion = "Data Source=(local); Initial Catalog = ABELINBD; Integrated Security = SSPI; ";

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //public JsonResult validarUsuario(string Nombre, string Password)
        public JsonResult validarUsuario(string Nombre, string Password)
        {
            string nombre = "";

            SqlConnection con = new SqlConnection(conexion);
            con.Open();

            string sql = "select * from Usuarios u INNER JOIN Roles r ON u.Rol=r.Codigo where Nombre=@nombre and Password=@password";

            using (SqlCommand com = new SqlCommand(sql, con))
            {
                com.Parameters.Add(new SqlParameter("@nombre", Nombre));
                com.Parameters.Add(new SqlParameter("@password", Password));

                using (SqlDataReader resp = com.ExecuteReader())
                {
                    if (resp.Read())
                    {
                        {
                            
                            

                            if (Nombre.ToLower() == resp["Nombre"].ToString().ToLower() && Password.ToLower() == resp["Password"].ToString().ToLower())
                            {
                                nombre = resp["nombre"].ToString().ToLower();

                                Session["rol_usuario"] = (string)resp["Descripcion"];
                                Session["nombre_usuario"] = (string)resp["Nombre"];
                                Session["codigo_usuario"] = (string)resp["Codigo"];
                            }
                            
                        }
                    }
                }
            }

            return Json(nombre, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Home");
        }
    }
}
