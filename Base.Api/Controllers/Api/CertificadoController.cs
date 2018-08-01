using Base.Api.Utils;
using Base.Model.Dtos;
using Base.Model.Models;
using Base.Service.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace Base.Api.Controllers.Api
{
    public class CertificadoController : ApiController
    {
        private readonly ICertificadoService _certificadoService;

        public CertificadoController(ICertificadoService certificadoService)
        {
            _certificadoService = certificadoService;
        }


        //GET /api/certificado/GetCertificat
        [Route("api/certificado/GetCertificate/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCertificate(int id)
        {
            try
            {
                List<CertificadoDto> certificados = _certificadoService.Execute("Get", new CertificadoDto() { NoCcite=id }).ToList();

                if (certificados == null || certificados.Count <= 0)
                    return NotFound();               

                var certificado = new Certificado()
                {
                    IdCertificado = certificados[0].IdCertificado,
                    NoCcite = certificados[0].NoCcite,
                    EstadoCertificado = certificados[0].EstadoCertificado,
                    FechaExpedicion = certificados[0].FechaExpedicion,
                    FechaVencimiento = certificados[0].FechaVencimiento,
                    CodigoSeguridad = certificados[0].CodigoSeguridad,
                    NombreEmpresa= certificados[0].NombreEmpresa.Trim(),
                    Sucursal= certificados[0].Sucursal.Trim(),
                    Sustancias = (from cert in certificados select new Sustancia() { Nombre= cert.Sustancia,  Cantidad= cert.Cantidad, Unidad= cert.Unidad }).ToList()
                };
                

                return Ok(certificado);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
  
}
