using Base.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Dtos
{
    public class CertificadoDto
    {
        public long IdConsulta { get; set; }
        public int Codigo { get; set; }
        public string CodigoQr { get; set; }
        public Location Location { get; set; }
        public Certificado Certificado { get; set; }
        public List<Sustancia> Sustancias { get; set; }
    }
}
