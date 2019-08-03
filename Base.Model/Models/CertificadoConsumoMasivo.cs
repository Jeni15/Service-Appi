using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class CertificadoConsumoMasivo
    {          
        public long IdMovimiento { get; set; }    
        public string NombreEmpresa { get; set; }
        public string TipoDocumentoEmpresa { get; set; }
        public string DocumentoEmpresa { get; set; }       
        public string DireccionEmpresa { get; set; }
        public string DepartamentoEmpresa { get; set; }
        public string CiudadEmpresa { get; set; }

        public string NombreRepresentante { get; set; }
        public string TipoDocumentoRepresentante { get; set; }
        public string DocumentoRepresentante { get; set; }
        public string EmailRepresentante { get; set; }
        public string TelefonoRepresentante { get; set; }

        public string Sustancia { get; set; }
        public string Actividad { get; set; }
        public double Cantidad { get; set; }
        public string Unidad { get; set; }

        public string TipoDocumentoSoporte { get; set; }
        public string DocumentoSoporte { get; set; }
        public DateTime FechaEstimadaDesde { get; set; }
        public DateTime FechaEstimadaHasta { get; set; }        
        public string Uso { get; set; }

        public string NombreTercero { get; set; }
        public string TipoDocumentoTercero { get; set; }
        public string DocumentoTercero { get; set; }
        
        public string TelefonoTercero { get; set; }
        public string DepartamentoTercero { get; set; }
        public string CiudadTercero { get; set; }
        public string DireccionTercero { get; set; }


        /*-------------------------------------------
         * PROPIEDADES DE CONSULTA
         * ------------------------------------------*/



    }
}
