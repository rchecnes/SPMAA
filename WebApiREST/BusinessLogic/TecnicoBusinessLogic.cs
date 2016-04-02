using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiREST.ResourceAccess;
using WebApiREST.BusinessEntities.Interface;
using WebApiREST.Common;

namespace WebApiREST.BusinessLogic
{
    public class TecnicoBusinessLogic : ITecnicoBusinessLogic
    {
        private readonly ITecnicoDataAccess oITecnicoDataAccess;
        public TecnicoBusinessLogic(ITecnicoDataAccess oITecnicoDataAccess)
        {
            this.oITecnicoDataAccess = oITecnicoDataAccess;
        }
        public DTOTecnicoRespuesta ObtenerTecnico(string nombre)
        {
            DTOHeader oDTOHeader = new DTOHeader();
            List<DTOTecnico> oDTOTecnicoLista = new List<DTOTecnico>();
            DTOTecnicoRespuesta oDTOTecnicoRespuesta = new DTOTecnicoRespuesta();

            try
            {
                oDTOTecnicoLista = oITecnicoDataAccess.ObtenerTecnicos(nombre);
                oDTOTecnicoRespuesta.DTOTecnicoLista = oDTOTecnicoLista;
                if (oDTOTecnicoLista.Count == 0)
                {
                    oDTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                    oDTOHeader.DescRetorno = "No se encontraron técnicos.";
                }
                else
                {
                    oDTOHeader.CodigoRetorno = HeaderEnum.Correcto.ToString();
                    oDTOHeader.DescRetorno = string.Empty;
                }
            }
            catch (Exception ex)
            {
                oDTOHeader.CodigoRetorno = HeaderEnum.Incorrecto.ToString();
                oDTOHeader.DescRetorno = ex.Message;
            }

            oDTOTecnicoRespuesta.DTOHeader = oDTOHeader;
            return oDTOTecnicoRespuesta;
        }
    }
}