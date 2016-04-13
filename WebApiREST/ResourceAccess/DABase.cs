using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiREST.ResourceAccess
{
    public class DABase
    {
        //public static string cnsServicioODP = "Data Source=(local); Initial Catalog = ABELINBD; Integrated Security = SSPI;";
        public static string cnsServicioODP = @"Data Source = REDCELL07\SQLSERVER2014; Initial Catalog = ABELINBD; uid = sa; pwd = pass@Admin10;";
    }
}