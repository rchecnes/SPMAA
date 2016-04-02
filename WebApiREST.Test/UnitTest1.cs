using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace WebApiREST.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:47475/api/WSSentTecnico/Cesar Bedoya/");
            req.Method = "GET";
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader reader2 = new StreamReader(res.GetResponseStream());
            string alumnoJson2 = reader2.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Tecnico tecnicoObtenido = js.Deserialize<Tecnico>(alumnoJson2);
            Assert.AreEqual("00001", tecnicoObtenido.DTOTecnicoLista[0].Codigo.Trim());
            Assert.AreEqual("Cesar Bedoya", tecnicoObtenido.DTOTecnicoLista[0].Nombre.Trim());
        }
    }
}