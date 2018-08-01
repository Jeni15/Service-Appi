using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Dtos
{
    public class CertificadoDto
    {
        public long IdCertificado { get; set; }
        public long NoCcite { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CodigoSeguridad { get; set; }
        public string NombreEmpresa { get; set; }
        public string Sucursal { get; set; }
        public string EstadoCertificado { get; set; }
        public string Sustancia { get; set; }
        public double Cantidad { get; set; }
        public string Unidad { get; set; }
    }
}
