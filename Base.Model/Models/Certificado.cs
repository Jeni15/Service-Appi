using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class Certificado {          
        public long IdCertificado { get; set; }
        public int NoCcite { get; set; }        
        public string NombreEmpresa { get; set; }
        public string DocumentoEmpresa { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime FechaVencimiento { get; set; }        
        public string EstadoCertificado { get; set; }
        public string Periodicidad { get; set; }
        public string CodigoSeguridad { get; set; }

        /*-------------------------------------------
         * PROPIEDADES DE CONSULTA
         * ------------------------------------------*/
        


    }
}
