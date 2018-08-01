using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class Certificado
    {
        public long IdCertificado { get; set; }
        public long NoCcite { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CodigoSeguridad { get; set; }
        public string NombreEmpresa { get; set; }
        public string Sucursal { get; set; }
        public string EstadoCertificado { get; set; }
        public List<Sustancia> Sustancias { get; set; }

    }
}
