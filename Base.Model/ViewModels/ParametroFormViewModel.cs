using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class ParametroFormViewModel
    {
        public Parametro Parametro { get; set; }

        public List<Parametro> Parametros { get; set; }

        public List<Modelo> Modelos { get; set; }

        public List<ModeloVersion> ModelosVersiones { get; set; }

        public List<ModeloCaso> ModelosCasos { get; set; }

        public ModeloVersion ModeloVersion { get; set; }

        public ModeloCaso ModeloCaso { get; set; }
    }
}
