using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class LogConsulta
    {
        public double Id { get; set; }
        public string Tipo { get; set; } = "Ccite";

        public DateTime Fecha { get; set; }                
        public double IdCertificado { get; set; }
        public string IdUsuario { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
