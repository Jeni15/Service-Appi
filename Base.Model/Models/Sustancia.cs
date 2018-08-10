using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class Sustancia
    {
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public string Nombre { get; set; }
        public double Cantidad { get; set; }
        public string SiglaUnidad { get; set; }
        public string Sigla { get; set; }


        /*-------------------------------------------
         * PROPIEDADES DE CONSILTA
         * ------------------------------------------*/
        public int NoCcite { get; set; }
        public string CodigoSeguridad { get; set; }


    }
}
