using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using WebApiREST.ResourceAccess;
using WebApiREST.BusinessLogic;

namespace WebApiREST
{
    public class DependencyResolver
    {
        public static UnityContainer DIContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ITecnicoDataAccess, TecnicoDataAccess>();
            container.RegisterType<ITecnicoBusinessLogic, TecnicoBusinessLogic>();

            return container;
        }

    }
}