using Base.Api.Utils;
using Base.Model.Dtos;
using Base.Model.Models;
using Base.Service.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly ICertificadoConsumoMasivoService _certificadoConsumoMasivoService;
        private readonly ISustanciaService _sustanciaService;
        private readonly IUserTokenService _userTokenService;
        private readonly ILogConsultaService _logConsultaService;
        private readonly ITipoFallaService _tipoFallaService;
        private static TraceSource mySource = new TraceSource("SicoqApi");

        public CertificadoController(ICertificadoService certificadoService, 
                                     ISustanciaService sustanciaService, 
                                     IUserTokenService userTokenService, 
                                     ILogConsultaService logConsultaService,
                                     ITipoFallaService  tipoFallaService,
                                     ICertificadoConsumoMasivoService certificadoConsumoMasivoService)
        {
            _certificadoService = certificadoService;
            _sustanciaService = sustanciaService;
            _userTokenService = userTokenService;
            _logConsultaService = logConsultaService;
            _tipoFallaService = tipoFallaService;
            _certificadoConsumoMasivoService = certificadoConsumoMasivoService;
        }


        [Route("api/certificado/GetCertificateConsumoMasivo")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCertificateConsumoMasivo(CertificadoConsumoMasivoDto certificadoDto)
        {
            try
            {
                mySource.TraceInformation($"Inicio acción GetCertificateConsumoMasivo {JsonConvert.SerializeObject(certificadoDto)}");

                List<CertificadoConsumoMasivo> certificados = new List<CertificadoConsumoMasivo>();

                var userid = "";

                var token = GetToken();

                mySource.TraceInformation($"Validando Token {token}");

                var resultValidation = ValidateToken(token, ref userid);

                if (!resultValidation) { mySource.TraceEvent(TraceEventType.Information, 3, $"Token invalido {token}"); throw new Exception("Token invalido"); }

                mySource.TraceInformation($"Consultando certificado {JsonConvert.SerializeObject(certificadoDto)}");

                if ((string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo == 0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))
                    certificados = _certificadoConsumoMasivoService.Execute("ConsultaInformacionGeneralCertificado", new CertificadoConsumoMasivo() { IdMovimiento = Convert.ToInt32(certificadoDto.CodigoQr) }).ToList();
                else
                    certificados = _certificadoConsumoMasivoService.Execute("ConsultaInformacionGeneralCertificado", new CertificadoConsumoMasivo() { IdMovimiento = certificadoDto.Codigo }).ToList();
                
                mySource.TraceInformation($"Certificado obtenido {JsonConvert.SerializeObject(certificados)}");

                if (certificados == null || certificados.Count <= 0) return NotFound();

                mySource.TraceInformation($"Consultando sustancias ");

                certificadoDto.Certificado = new CertificadoConsumoMasivo()
                {

                    IdMovimiento = certificados[0].IdMovimiento,
                    NombreEmpresa = certificados[0].NombreEmpresa,
                    TipoDocumentoEmpresa = certificados[0].TipoDocumentoEmpresa,
                    DocumentoEmpresa = certificados[0].DocumentoEmpresa,
                    DireccionEmpresa = certificados[0].DireccionEmpresa,
                    DepartamentoEmpresa = certificados[0].DepartamentoEmpresa,
                    CiudadEmpresa = certificados[0].CiudadEmpresa,

                    NombreRepresentante = certificados[0].NombreRepresentante,
                    TipoDocumentoRepresentante = certificados[0].TipoDocumentoRepresentante,
                    DocumentoRepresentante = certificados[0].DocumentoRepresentante,
                    EmailRepresentante = certificados[0].EmailRepresentante,
                    TelefonoRepresentante = certificados[0].TelefonoRepresentante,

                    Sustancia = certificados[0].Sustancia,
                    Actividad = certificados[0].Actividad,
                    Cantidad = certificados[0].Cantidad,
                    Unidad = certificados[0].Unidad,

                    TipoDocumentoSoporte = certificados[0].TipoDocumentoSoporte,
                    DocumentoSoporte = certificados[0].DocumentoSoporte,
                    FechaEstimadaDesde = certificados[0].FechaEstimadaDesde,
                    FechaEstimadaHasta = certificados[0].FechaEstimadaHasta,
                    Uso = certificados[0].Uso,

                    TipoDocumentoTercero = certificados[0].TipoDocumentoTercero,
                    DocumentoTercero = certificados[0].DocumentoTercero,
                    TelefonoTercero = certificados[0].TelefonoTercero,
                    DepartamentoTercero = certificados[0].DepartamentoTercero,
                    CiudadTercero = certificados[0].CiudadTercero,
                    DireccionTercero = certificados[0].DireccionTercero

                };


                var logId = _logConsultaService.Create(new LogConsulta() { IdCertificado = certificadoDto.Codigo, Tipo="Consumo Masivo", IdUsuario = userid, Latitude = certificadoDto.Location.Latitude, Longitude = certificadoDto.Location.Longitude });

                certificadoDto.IdConsulta = Convert.ToInt64(logId);

                mySource.TraceEvent(TraceEventType.Information, 10, $"Retorno {JsonConvert.SerializeObject(certificadoDto)}");

                return Ok(certificadoDto);

            }
            catch (Exception ex)
            {
                mySource.TraceEvent(TraceEventType.Error, 99, ex.Message);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                mySource.Flush();
            }

        }



        //GET /api/certificado/GetCertificat
        //[Route("api/certificado/GetCertificate/{id}")]
        [Route("api/certificado/GetCertificate")]
        [HttpPost]
        public async Task<IHttpActionResult> GetCertificate(CertificadoDto certificadoDto)
        {
            try
            {
                mySource.TraceInformation($"Inicio acción GetCertificate {JsonConvert.SerializeObject(certificadoDto)}");

                List <Certificado> certificados = new List<Certificado>();

                List<Sustancia> sustancias = new List<Sustancia>();

                var userid = "";

                var token = GetToken();

                mySource.TraceInformation($"Validando Token {token}");

                var resultValidation = ValidateToken(token, ref userid);

                if (!resultValidation) { mySource.TraceEvent(TraceEventType.Information, 3, $"Token invalido {token}");  throw new Exception("Token invalido"); }

                mySource.TraceInformation( $"Consultando certificado {JsonConvert.SerializeObject(certificadoDto)}");

                if ( (string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo == 0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))                
                    certificados = _certificadoService.Execute("ConsultaInformacionGeneralCcitePorQr", new Certificado() { CodigoSeguridad = certificadoDto.CodigoQr }).ToList();                
                else                
                    certificados = _certificadoService.Execute("ConsultaInformacionGeneralCcite", new Certificado() { NoCcite = certificadoDto.Codigo }).ToList();

                mySource.TraceInformation($"Certificado obtenido {JsonConvert.SerializeObject(certificados)}");

                if (certificados == null || certificados.Count <= 0) return NotFound();

                mySource.TraceInformation($"Consultando sustancias ");

                if ( (string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo == 0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))
                    sustancias = _sustanciaService.Execute("ConsultaSustanciasCcitePorQr", new Sustancia() { CodigoSeguridad = certificadoDto.CodigoQr }).ToList();
                else
                    sustancias = _sustanciaService.Execute("ConsultaSustanciasCcite", new Sustancia() { NoCcite = certificadoDto.Codigo }).ToList();

                mySource.TraceInformation($"sustancias obtenidas {JsonConvert.SerializeObject(sustancias)}");

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
                if ((string.IsNullOrEmpty(certificadoDto.Codigo.ToString()) || certificadoDto.Codigo ==0) && !string.IsNullOrEmpty(certificadoDto.CodigoQr))
                    idCertificado = GetCertificateIdFromCcitePorQr(certificadoDto.CodigoQr); 
                else
                    idCertificado = GetCertificateIdFromCcite(certificadoDto.Codigo);


                if ( idCertificado == 0) throw new Exception ("Certificado no encontrado");

                var logId = _logConsultaService.Create(new LogConsulta() { IdCertificado = idCertificado, IdUsuario = userid, Latitude = certificadoDto.Location.Latitude, Longitude = certificadoDto.Location.Longitude });

                certificadoDto.IdConsulta = Convert.ToInt64(logId);

                mySource.TraceEvent(TraceEventType.Information, 10, $"Retorno {JsonConvert.SerializeObject(certificadoDto)}");

                return Ok(certificadoDto);

            }
            catch (Exception ex)
            {
                mySource.TraceEvent(TraceEventType.Error, 99, ex.Message);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
               mySource.Flush();               
            }
            
        }

        //GET /api/certificado/GetCertificat
        //[Route("api/certificado/GetTipoFallas/")]
        [Route("api/certificado/GetTipoFallas")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTipoFallas(TipoFallaDto tipoFallaDto)
        {
            try
            {
                mySource.TraceInformation($"Inicio acción GetTipoFallas {JsonConvert.SerializeObject(tipoFallaDto)}");
             
                var userid = "";

                var token = GetToken();

                mySource.TraceInformation($"Validando Token {token}");

                var resultValidation = ValidateToken(token, ref userid);

                if (!resultValidation) { mySource.TraceEvent(TraceEventType.Information, 3, $"Token invalido {token}"); throw new Exception("Token invalido"); }

                mySource.TraceInformation($"Consultando TipoFallas");

                var tipoFallas = _tipoFallaService.GetAll();

                return Ok(tipoFallas);

            }
            catch (Exception ex)
            {
                mySource.TraceEvent(TraceEventType.Error, 99, ex.Message);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                mySource.Flush();
            }

        }


        //GET /api/certificado/GetCertificat
        //[Route("api/certificado/GetCertificate/{id}")]
        [Route("api/certificado/CreateTipoFallaCertificado")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateTipoFallaCertificado(List<TipoFalla> tipoFallas)
        {
            try
            {
                mySource.TraceInformation($"Inicio acción CreateTipoFallaCertificado {JsonConvert.SerializeObject(tipoFallas)}");

                List<Certificado> certificados = new List<Certificado>();

                List<Sustancia> sustancias = new List<Sustancia>();

                var userid = "";

                var token = GetToken();

                mySource.TraceInformation($"Validando Token {token}");

                var resultValidation = ValidateToken(token, ref userid);

                if (!resultValidation) { mySource.TraceEvent(TraceEventType.Information, 3, $"Token invalido {token}"); throw new Exception("Token invalido"); }

                mySource.TraceInformation($"Guardando tipos de fallas {JsonConvert.SerializeObject(tipoFallas)}");


                foreach(var falla in tipoFallas)
                {                    
                    _tipoFallaService.Execute("DetalleInsert", falla);
                }
                                               
                return Created("","");

            }
            catch (Exception ex)
            {
                mySource.TraceEvent(TraceEventType.Error, 99, ex.Message);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                mySource.Flush();
            }

        }

        private string GetToken(string tokenType="Bearer")
        {
            mySource.TraceEvent(TraceEventType.Information, 2, $"Recuperando token {tokenType}");

            string authHeader = HttpContext.Current.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith(tokenType))
            {
                mySource.TraceEvent(TraceEventType.Information, 2, "Token vacío");
                throw new Exception("Token vacío");
            }

            string token= authHeader.Substring($"{tokenType} ".Length).Trim();

            mySource.TraceEvent(TraceEventType.Information, 2, $"Token recuperado {token}");

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

        private long GetCertificateIdFromCcite(int ccite)
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
