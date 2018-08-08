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
        public long Codigo { get; set; }
        public Location Location { get; set; }
        public Certificado Certificado { get; set; }
        public List<Sustancia> Sustancias { get; set; }
    }
}
