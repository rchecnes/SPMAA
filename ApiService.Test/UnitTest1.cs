using System;
using System.Text;
using ApiService.Test.PlanificacionesSRV;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ApiService.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PlanificacionClient proxy = new PlanificacionClient();

            List<Planificaciones> oListPlanificacion = new List<Planificaciones>();
            Planificaciones[] oListPlanificacionProxy;
            Planificaciones oPlanificacion = new Planificaciones();
            oPlanificacion.Contrato = new Contratos();
            oPlanificacion.Contrato.Cliente = new Clientes();
            oPlanificacion.Contrato.Tecnico = new Tecnicos();

            oPlanificacion.Codigo = "00001";
            oPlanificacion.CodigoContrato = "00001";
            oPlanificacion.Visita = new DateTime(2015, 02, 01);
            oPlanificacion.CodigoTrabajo = "00011";
            oPlanificacion.Contrato.Inicio = new DateTime(2015, 02, 01);
            oPlanificacion.Contrato.Fin = new DateTime(2016, 01, 01);
            oPlanificacion.Contrato.Cliente.RazonSocial = "Empresa Maderera Sullana S.A.";
            oPlanificacion.Contrato.Tecnico.Nombre = "Diego Reyes";
            oListPlanificacion.Add(oPlanificacion);

            Planificaciones oFiltro = new Planificaciones();
            oFiltro.Contrato = new Contratos();
            oFiltro.Contrato.Codigo = "00001";
            oListPlanificacionProxy = proxy.ObtenerPlanificaciones(oFiltro);

            Assert.AreEqual(oListPlanificacionProxy[0].Contrato.Cliente.RazonSocial, oListPlanificacionProxy[0].Contrato.Cliente.RazonSocial);
        }
    }
}