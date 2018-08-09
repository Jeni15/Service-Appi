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
        private readonly ISustanciaService _sustanciaService;
        private readonly IUserTokenService _userTokenService;
        private readonly ILogConsultaService _logConsultaService;

        public CertificadoController(ICertificadoService certificadoService, ISustanciaService sustanciaService, IUserTokenService userTokenService, ILogConsultaService logConsultaService)
        {
            _certificadoService = certificadoService;
            _sustanciaService = sustanciaService;
            _userTokenService = userTokenService;
            _logConsultaService = logConsultaService;
        }


        //GET /api/certificado/GetCertificat
        //[Route("api/certificado/GetCertificate/{id}")]
        [Route("api/certificado/GetCertificate")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCertificate(CertificadoDto certificadoDto)
        {
            try
            {
                List<Certificado> certificados = new List<Certificado>();

                List<Sustancia> sustancias = new List<Sustancia>();

                var userid = "";

                var token = GetToken();

                var resultValidation = ValidateToken(token, ref userid);

                if (!resultValidation) throw new Exception ("Token invalido");

                if ( (string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo == 0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))                
                    certificados = _certificadoService.Execute("ConsultaInformacionGeneralCcitePorQr", new Certificado() { CodigoSeguridad = certificadoDto.CodigoQr }).ToList();                
                else                
                    certificados = _certificadoService.Execute("ConsultaInformacionGeneralCcite", new Certificado() { NoCcite = certificadoDto.Codigo }).ToList();
                

                if (certificados == null || certificados.Count <= 0) return NotFound();

                if ( (string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo == 0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))
                    sustancias = _sustanciaService.Execute("ConsultaSustanciasCcitePorQr", new Sustancia() { CodigoSeguridad = certificadoDto.CodigoQr }).ToList();
                else
                    sustancias = _sustanciaService.Execute("ConsultaSustanciasCcite", new Sustancia() { NoCcite = certificadoDto.Codigo }).ToList();


                certificadoDto.Certificado = new Certificado()
                {                 
                    NoCcite = certificados[0].NoCcite,
                    NombreEmpresa = certificados[0].NombreEmpresa.Trim(),
                    DocumentoEmpresa = certificados[0].DocumentoEmpresa.Trim(),                    
                    FechaExpedicion = certificados[0].FechaExpedicion,
                    FechaVencimiento = certificados[0].FechaVencimiento,
                    EstadoCertificado = certificados[0].EstadoCertificado,
                    Periodicidad = certificados[0].Periodicidad,
                    CodigoSeguridad = certificados[0].CodigoSeguridad                    
                };

                certificadoDto.Sustancias = sustancias;

                long idCertificado = 0;
                if ((string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo == 0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))
                    idCertificado = GetCertificateIdFromCcitePorQr(certificadoDto.CodigoQr); 
                else
                    idCertificado = GetCertificateIdFromCcite(certificadoDto.Codigo);


                if ( idCertificado == 0) throw new Exception ("Certificado no encontrado");

                var logId = _logConsultaService.Create(new LogConsulta() { IdCertificado = idCertificado, IdUsuario = userid, Latitude = certificadoDto.Location.Latitude, Longitude = certificadoDto.Location.Longitude });

                certificadoDto.IdConsulta = Convert.ToInt64(logId);

                return Ok(certificadoDto);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private string GetToken(string tokenType="Bearer")
        {           
            string authHeader = HttpContext.Current.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith(tokenType))
                throw new Exception("Token vacío");

            string token= Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(authHeader.Substring($"{tokenType} ".Length).Trim()));
          
            return token;

        }

        private bool ValidateToken(string token, ref string userid)
        {
            var result = _userTokenService.Execute("ValidateToken", new UserToken() { IdToken = token }).ToList();

            if (result != null && result.Count() > 0)
            {
                if (!string.IsNullOrEmpty(result[0].IdToken))
                {
                    userid = result[0].IdUsuario;
                    return true;
                }
            }
            
            return false;
        }

        private long GetCertificateIdFromCcite(long ccite)
        {
            var result = _certificadoService.Execute("GetCertificateIdFromCcite", new Certificado() { NoCcite = ccite }).ToList();

            if (result != null && result.Count() > 0)
            {
               return result[0].IdCertificado;                
            }

            return 0;
        }

        private long GetCertificateIdFromCcitePorQr(string qr)
        {
            var result = _certificadoService.Execute("GetCertificateIdFromCcitePorQr", new Certificado() { CodigoSeguridad = qr }).ToList();

            if (result != null && result.Count() > 0)
            {
                return result[0].IdCertificado;
            }

            return 0;
        }
    }
  
}
