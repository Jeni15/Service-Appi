using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Model.Models
{
    public class ParametroFormViewModel
    {
        public List<Parametro> Parametros { get; set; }

        public Parametro Parametro { get; set; }

        public List<Modelo> Modelos { get; set; }

        public List<Version> Versiones { get; set; }

        public List<Set> Sets { get; set; }
    }
}
